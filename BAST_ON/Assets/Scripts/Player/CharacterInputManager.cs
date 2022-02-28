using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputManager : MonoBehaviour
{
    #region references
    private CharacterMovementController _myMovementController;
    private CharacterAttackController _myAttackController;
    #endregion

    #region properties
    private float _horizontalInput;
    private float _jumpInput;
    private float _attackInput;
    private float _horizontalAttackInput;
    private float _verticalAttackInput;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myMovementController = GetComponent<CharacterMovementController>();
        _myAttackController = GetComponent<CharacterAttackController>();
    }

    // Update is called once per frame
    void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _jumpInput = Input.GetAxis("Jump");
        _attackInput = Input.GetAxis("Fire1");


        if(_horizontalInput != 0)
        {
            _myMovementController.SetMovementDirection(_horizontalInput);
        }
        if (_jumpInput != 0)
        {
            _myMovementController.JumpRequest();
        }
        if (_attackInput != 0)
        {
            _myAttackController.Bastonazo(_horizontalAttackInput, _verticalAttackInput);
        }

    }
}
