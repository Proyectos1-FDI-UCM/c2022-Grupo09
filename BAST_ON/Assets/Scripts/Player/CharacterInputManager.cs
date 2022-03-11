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
    private float _previusVerticalInput = 0;
    private float _jumpInput;
    private float _attackInput;
    private float _horizontalAttackInput;
    private float _verticalAttackInput;
    private float _dashInput;
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
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        _jumpInput = Input.GetAxis("Jump");
        _attackInput = Input.GetAxis("Fire1");
        _myAnimator.SetFloat("MovementDirection", Mathf.Abs(_horizontalInput));

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

        _horizontalAttackInput = Input.GetAxis("Horizontal");
        _verticalAttackInput = Input.GetAxis("Vertical");
        _dashInput = Input.GetAxis("Dash");
        NormalizeAttackInput(ref _horizontalAttackInput, ref _verticalAttackInput);
        if (_attackInput != 0)
        {
            _myAttackController.Bastonazo(Mathf.Abs(_horizontalAttackInput), _verticalAttackInput);
        }
        if (_dashInput > 0)
        {
            _myAttackController.Dash();
        }

        _previusVerticalInput = _verticalInput;
    }
}
