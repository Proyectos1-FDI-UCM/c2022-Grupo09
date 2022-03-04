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
    #endregion

    #region properties
    private Vector3 _movementDirection = Vector3.zero;
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
    
    public void JumpRequest()
    {
        if (_myFloorDetector.IsGrounded())
        {
            _myRigidbody.velocity = new Vector2(_myRigidbody.velocity.x, _jumpSpeed);
            _myCameraController.ResetVerticalOffset();
        }
    }

    ///<summary>
    ///Tentativa de función para añadir la fuerza al personaje. Si al final vamos a hacerlo por CharacterController, habrá que cambiar
    ///</summary>
    public void addRepelForce(Vector2 forceDirection){
        //_myRigidbody2D.AddForce(forceDirection , ForceMode2D.Impulse);
        
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
        _myTransform.rotation = Quaternion.identity;
        if (_movementDirection.x < 0) _myTransform.Rotate(Vector3.up, 180f);
        // Movimiento del personaje
        _movementDirection.x *= _speedMovement;
        _movementDirection.y = _myRigidbody.velocity.y;
        _myRigidbody.velocity = _movementDirection;
        _movementDirection = Vector3.zero;

    }
}
