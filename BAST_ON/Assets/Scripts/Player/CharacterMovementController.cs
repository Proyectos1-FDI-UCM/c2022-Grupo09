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
    // Gravity value applied to player
    [SerializeField]
    private float _gravity = 1.0f;
    // Maximum falling speed
    [SerializeField]
    private float _fallSpeed = 10.0f;
    #endregion

    #region properties
    private Vector3 _movementDirection = Vector3.zero;
    private bool _onFloor = false;
    private float _elapsedJumpTime;
    // Stores current vertical speed value
    private float _verticalSpeed = 0;
    #endregion

    #region references
    private Transform _myTransform;
    [SerializeField]
    private GameObject _myCamera;
    private CameraController _myCameraController;
    private CharacterAttackController _myAttackController;
    #endregion

    #region methods
    public void SetFloorDetector(bool a)
    {
        _onFloor = a;
        if (a) 
        {
            _elapsedJumpTime = 0;
            _verticalSpeed = 0; 
        }
    }
    public void SetMovementDirection(float direction)
    {
        _movementDirection.x = direction;
    }
    
    public void JumpRequest()
    {
        if (_onFloor)
        {
            _verticalSpeed = _jumpSpeed;
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
    }

    // Update is called once per frame
    void Update()
    {
        // Jump
        if (!_onFloor)
        {
            _verticalSpeed += -_gravity * _elapsedJumpTime;
            _elapsedJumpTime += Time.deltaTime;
        }
        _movementDirection.y = _verticalSpeed;

        // Offset cámara y dirección predeterminada del ataque
        if (_movementDirection.x != 0)
        {
            _myCameraController.SetOffset(_movementDirection.normalized);
            _myAttackController.SetDefaultDirection(_movementDirection.x);
        }

        // Rotación ajustada para dirección de la animación
        _myTransform.rotation = Quaternion.identity;
        if (_movementDirection.x < 0) _myTransform.Rotate(new Vector3(0, 180, 0));
        // Movimiento del personaje (Siempre positivo porque se ha rotado con la animación)
        _movementDirection.x = Mathf.Abs(_movementDirection.x);
        _myTransform.Translate(_movementDirection * _speedMovement * Time.deltaTime);
        _movementDirection = Vector3.zero;

    }
}
