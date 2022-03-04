using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _speedMovement = 1f;

    [SerializeField]
    private float _jumpForce = 5f;
    [SerializeField]
    private int _nJumps;
    [SerializeField]
    private int _limitJumps = 1;
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
    private Rigidbody2D _myRigidbody2D;
    #endregion

    #region methods
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor")
        {
            _nJumps = 0;
        }
    }
    public void SetMovementDirection(float direction)
    {
        _movementDirection.x = direction;
    }
    
    public void JumpRequest()
    {
        if (_nJumps < _limitJumps)
        {
            _myRigidbody2D.AddForce(new Vector2(0f, _jumpForce), ForceMode2D.Impulse);
            _nJumps++;
        }
    }

    ///<summary>
    ///Tentativa de función para añadir la fuerza al personaje. Si al final vamos a hacerlo por CharacterController, habrá que cambiar
    ///</summary>
    public void addRepelForce(Vector2 forceDirection){
        _myRigidbody2D.AddForce(forceDirection , ForceMode2D.Impulse);
        
    }

    
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _nJumps = 0;
        _myTransform = transform;
        _myCameraController = _myCamera.GetComponent<CameraController>();
        _myAttackController = GetComponent<CharacterAttackController>();
        _myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _myTransform.rotation = Quaternion.identity;
        if (_movementDirection.x < 0) _myTransform.Rotate(new Vector3(0, 180, 0));
        _movementDirection.x = Mathf.Abs(_movementDirection.x);
        _myTransform.Translate(_movementDirection * _speedMovement * Time.deltaTime);
        if (_movementDirection.x != 0)
        {
            _myCameraController.SetOffset(_movementDirection.normalized);
            _myAttackController.SetDefaultDirection(_movementDirection.x);
        }
        _movementDirection = Vector3.zero;
    }
}
