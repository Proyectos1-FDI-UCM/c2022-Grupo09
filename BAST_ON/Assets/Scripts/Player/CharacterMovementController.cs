using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _speedMovement = 1f;
    [SerializeField]
    private float _onWallGravityReduce = 3f;
    [SerializeField]
    private float _jumpSpeed = 1f;
    [SerializeField]
    private float _bastonImpulse = 1f;
    [SerializeField]
    private float _wallJumpBlockMovement = 0.5f;
    [SerializeField]
    private float _verticalDamageImpulse = 2f;
    [SerializeField]
    private float _horizontalDamageImpulse = 2f;
    #endregion

    #region properties
    /// <summary>
    /// Dirección del movimiento del jugador
    /// </summary>
    private Vector2 _movementDirection = Vector2.zero;
    /// <summary>
    /// Dirección en la que es impulsado el jugador por un salto o un golpe con el bastón.
    /// </summary>
    private Vector2 _impulseDirection = Vector2.zero;
    /// <summary>
    /// Variable auxiliar que cuenta el tiempo transcurrido desde un impulso aplicado (salto o bastón) para ir reduciendo la velocidad aplicada por este con el tiempo.
    /// </summary>
    private float _impulseElapsedTime = 1f;
    /// <summary>
    /// Variable auxiliar que cuenta el tiempo que el jugador lleva en el aire para aplicarlo a la gravedad.
    /// </summary>
    private float _onAirElasedTime = 0f;

    private float _gravityReducer;
    /// <summary>
    /// Réplica de la gravedad que se le aplicaría al Rigidbody2D para así poder mover al jugador de forma consistente con el método MovePosition().
    /// </summary>
    private Vector2 _gravity = Vector2.zero;

    private float _originalSpeedMovement;

    private bool _attackedWall = false;

    private Vector2 _movement;

    private float _wallAttackElapsedTime = 0;
    #endregion

    #region references
    private Transform _myTransform;
    [SerializeField]
    private GameObject _myCamera;
    private CameraController _myCameraController;
    private CharacterAttackController _myAttackController;
    private FloorDetector _myFloorDetector;
    private Rigidbody2D _myRigidbody;
    private WallDetector _myWallDetector;
    private Animator _myAnimator;
    #endregion

    #region methods
    /// <summary>
    /// Asigna a la dirección del movimiento la recibida por el input.
    /// </summary>
    /// <param name="direction"></param>
    public void SetMovementDirection(float direction)
    {
        _movementDirection.x = direction;
    }

    ///<summary>
    ///Hace que el personaje salte si está en el suelo.
    ///</summary>
    public void JumpRequest()
    {
        if (_myFloorDetector.IsGrounded())
        {
            _impulseDirection.y = _jumpSpeed;
            _impulseElapsedTime = 1f;
            _myCameraController.ResetVerticalOffset();
        }
    }

    ///<summary>
    ///Añade el impulso recibido del bastón al personaje si está en el aire.
    ///</summary>
    public void ImpulseRequest(Vector2 forceDirection)
    {
        if (!_myFloorDetector.IsGrounded())
        {
            _impulseDirection = forceDirection * _bastonImpulse;
            _impulseElapsedTime = 1f;
            // Reset del tiempo en el aire para dar mejor sensación de juego
            _onAirElasedTime = 0f;
            _myCameraController.ResetVerticalOffset();
        }
    }
    public void DamageImpulseRequest(Vector3 damagerPosition)
    {
        if(_movement != Vector2.zero)
        {
            _impulseDirection.y = -_movement.y * _verticalDamageImpulse;
            _impulseDirection.x = -_movement.x * _horizontalDamageImpulse;
        }
        else
        {
            _impulseDirection.x = (_myTransform.position.x - damagerPosition.x) * _horizontalDamageImpulse;
            _impulseDirection.y = (_myTransform.position.y - damagerPosition.y) * _verticalDamageImpulse;
        }
        _attackedWall = true;
        _impulseElapsedTime = 1f;
        _onAirElasedTime = 0f;
    }
    public void WallWasAttacked(bool attacked)
    {
        _attackedWall = attacked;
    }
    //Método para aumentar la velocidad del jugaador en caso de que coja un kiwi
    public void PlusVelocity(float newVelocity)
    {
        _speedMovement = _speedMovement * newVelocity;
        _originalSpeedMovement = _speedMovement;
        _myRigidbody.MovePosition(_myRigidbody.position + _gravity + ((_movementDirection * _speedMovement + _impulseDirection) * Time.fixedDeltaTime));
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        _myCameraController = _myCamera.GetComponent<CameraController>();
        _myAttackController = GetComponent<CharacterAttackController>();
        _myFloorDetector = GetComponent<FloorDetector>();
        _myRigidbody = GetComponent<Rigidbody2D>();
        _myWallDetector = GetComponent<WallDetector>();

        _myAnimator = GetComponent<Animator>();

        _originalSpeedMovement = _speedMovement;
    }

    // Update is called once per frame
    void Update()
    {
        // Offset cámara y dirección predeterminada del ataque
        if (_movementDirection.x != 0)
        {
            _myCameraController.SetOffset(_movementDirection.normalized);
            _myAttackController.SetDefaultDirection(_movementDirection.x);
        }

        // Rotación ajustada para dirección de la animación
        if (_movementDirection.x > 0) _myTransform.rotation = Quaternion.identity;
        else if (_movementDirection.x < 0)
        {
            _myTransform.rotation = Quaternion.identity;
            _myTransform.Rotate(Vector3.up, 180f);
        }

        if (_wallAttackElapsedTime > _wallJumpBlockMovement) 
        {
            _attackedWall = false;
            _wallAttackElapsedTime = 0f;
        }
        else if (_attackedWall) _wallAttackElapsedTime += Time.deltaTime;

        _myAnimator.SetBool("Wall", _myWallDetector.isInWall());
        _myAnimator.SetBool("Grounded", _myFloorDetector.IsGrounded());
        _myAnimator.SetFloat("VerticalDirection", _movement.y);
    }

    private void FixedUpdate()
    {
        // Impulso que va reduciendo a cada iteración
        _impulseDirection = _impulseDirection / _impulseElapsedTime;

        // Reductor gravedad si está en la pared
        if (_myWallDetector.isInWall() && !_myFloorDetector.IsGrounded()) _gravityReducer = _onWallGravityReduce;
        else _gravityReducer = 1;

        // Calculo de la gravedad
        _gravity = (Vector2.down * _myRigidbody.gravityScale * _onAirElasedTime) / _gravityReducer;

        // Velocidad a 0 si acaba de golpear una pared
        if (_attackedWall) _speedMovement = 0;
        else _speedMovement = _originalSpeedMovement;

        // Movimiento del personaje
        _movement = _gravity + ((_movementDirection * _speedMovement + _impulseDirection) * Time.fixedDeltaTime);
        _myRigidbody.MovePosition(_myRigidbody.position + _movement);

        //Sonido de pasos
        if (_myFloorDetector.IsGrounded() && _movement != Vector2.zero)
        {
            GetComponent<AudioSource>().enabled = true;
        }
        else
        {
            GetComponent<AudioSource>().enabled = false;
        }

        // Contador del tiempo en el aire
        if (!_myFloorDetector.IsGrounded()) _onAirElasedTime += Time.fixedDeltaTime;
        else _onAirElasedTime = 0f;

        // Contador para reducir el impulso
        if (_impulseElapsedTime < _impulseDirection.magnitude) _impulseElapsedTime += Time.fixedDeltaTime;
        else _impulseDirection = Vector2.zero;

        // Reset del movimiento
        _movementDirection = Vector2.zero;
      
    }
}