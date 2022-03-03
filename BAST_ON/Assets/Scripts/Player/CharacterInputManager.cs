using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputManager : MonoBehaviour
{
    #region references
    private CharacterMovementController _myMovementController;
    private CharacterAttackController _myAttackController;
    [SerializeField]
    private GameObject _myCamera;
    private CameraController _myCameraController;
    #endregion

    #region properties
    private float _horizontalInput;
    private float _verticalInput;
    private float _previusVerticalInput = 0;
    private float _jumpInput;
    private float _attackInput;
    private float _horizontalAttackInput;
    private float _verticalAttackInput;
    #endregion

    #region methods
    private void NormalizeAttackInput(ref float horizontal, ref float vertical)
    {
        Vector2 attackDirectionInput;
        attackDirectionInput.x = horizontal;
        attackDirectionInput.y = vertical;

        attackDirectionInput.Normalize();
        horizontal = attackDirectionInput.x;
        vertical = attackDirectionInput.y;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myMovementController = GetComponent<CharacterMovementController>();
        _myAttackController = GetComponent<CharacterAttackController>();
        _myCameraController = _myCamera.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        _jumpInput = Input.GetAxis("Jump");
        _attackInput = Input.GetAxis("Fire1");

        if(_horizontalInput != 0)
        {
            _myMovementController.SetMovementDirection(_horizontalInput);
        }
        else if (_verticalInput != 0)
        {
            _myCameraController.SetVerticalOffset(_verticalInput);
        }
        else if (_verticalInput != _previusVerticalInput)
        {
            _myCameraController.ResetVerticalOffset();
        }
        if (_jumpInput != 0)
        {
            _myMovementController.JumpRequest();
        }

        _horizontalAttackInput = Input.GetAxis("HorizontalAttack"); // Eje horizontal joystick derecho
        _verticalAttackInput = Input.GetAxis("VerticalAttack");   // Eje vertical joystick derecho
        NormalizeAttackInput(ref _horizontalAttackInput, ref _verticalAttackInput);
        if (_attackInput != 0)
        {
            _myAttackController.Bastonazo(_horizontalAttackInput, _verticalAttackInput);
        }


        _previusVerticalInput = _verticalInput;
    }
}
