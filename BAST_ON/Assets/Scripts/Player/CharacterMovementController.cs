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
    private Vector2 _movementDirection = Vector2.zero;
    private Vector2 _impulseDirection = Vector2.zero;
    private float _impulseElapsedTime = 1f;
    private float _onAirElasedTime = 0f;
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
    #endregion

    #region methods
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
            //_myRigidbody.velocity = new Vector2(_myRigidbody.velocity.x, _jumpSpeed);
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
        // Movimiento del personaje
    }

    private void FixedUpdate()
    {
        _movementDirection.x *= _speedMovement;
        //_movementDirection.y = _myRigidbody.position.y;

        _impulseDirection = _impulseDirection / _impulseElapsedTime;

        _gravity = (Vector2.down * _myRigidbody.gravityScale * _onAirElasedTime);

        // Movimiento del personaje
        _myRigidbody.MovePosition(_gravity + _myRigidbody.position + ((_movementDirection + _impulseDirection) * Time.fixedDeltaTime));

        if (!_myFloorDetector.IsGrounded()) _onAirElasedTime += Time.fixedDeltaTime;
        else _onAirElasedTime = 0f;
        if (_impulseElapsedTime < _impulseDirection.magnitude) _impulseElapsedTime += Time.fixedDeltaTime;
        else _impulseDirection = Vector2.zero;
        _movementDirection = Vector2.zero;
    }
}
