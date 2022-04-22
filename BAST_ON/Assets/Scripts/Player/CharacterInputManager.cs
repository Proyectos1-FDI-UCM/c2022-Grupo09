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
    private Animator _myAnimator;
    #endregion

    #region properties
    private float _horizontalInput;
    private float _verticalInput;
    private float _previousVerticalInput = 0;
    private float _jumpInput;
    private float _previousJumpInput;
    private float _attackInput;
    private float _previousAttackInput;
    private float _dashInput;
    private float _previousDashInput;
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
        _myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Ejes de input
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        _jumpInput = Input.GetAxis("Jump");
        _attackInput = Input.GetAxis("Fire1");
        _dashInput = Input.GetAxis("Dash");
        // Animación de movimiento
        _myAnimator.SetFloat("MovementDirection", Mathf.Abs(_horizontalInput));

        if (_horizontalInput != 0)
        {
            _myMovementController.SetMovementDirection(_horizontalInput);
        }
        else if (_verticalInput != 0)
        {
            _myCameraController.SetVerticalOffset(_verticalInput);
        }
        else if (_verticalInput != _previousVerticalInput)
        {
            _myCameraController.ResetVerticalOffset();
        }
        if (_jumpInput != 0 && _jumpInput != _previousJumpInput)
        {
            _myMovementController.JumpRequest();
        }

        NormalizeAttackInput(ref _horizontalInput, ref _verticalInput);
        if (_attackInput != 0 && _attackInput != _previousAttackInput)
        {
            _myAttackController.Golpe(Mathf.Abs(_horizontalInput), _verticalInput);
        }
        if (_dashInput > 0 && _dashInput != _previousDashInput)
        {
            _myAttackController.Dash();
        }

        _previousVerticalInput = _verticalInput;
        _previousAttackInput = _attackInput;
        _previousJumpInput = _jumpInput;
        _previousDashInput = _dashInput;
    }
}