using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _speedMovement = 1f;

    [SerializeField]
    private float _jumpSpeed = 1f;
    [SerializeField]
    private float _bastonImpulse = 1f;
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
        }
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
    }

    private void FixedUpdate()
    {
        // Impulso que va reduciendo a cada iteración
        _impulseDirection = _impulseDirection / _impulseElapsedTime;

        if (_myWallDetector.isInWall())_gravityReducer = 0.5f;

        else _gravityReducer = 1;

        _gravity = (Vector2.down * _myRigidbody.gravityScale * _onAirElasedTime) / _gravityReducer;
        // Movimiento del personaje
        _myRigidbody.MovePosition(_myRigidbody.position + _gravity + ((_movementDirection * _speedMovement + _impulseDirection) * Time.fixedDeltaTime));

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
