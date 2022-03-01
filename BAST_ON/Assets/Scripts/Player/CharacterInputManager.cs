using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputManager : MonoBehaviour
{
    #region references
    private CharacterMovementController _myMovementController;
    #endregion

    #region parameters
    [SerializeField]
    private int _nJumps;

    [SerializeField]
    private int _limitJumps = 1;
    #endregion

    #region properties
    private float _horizontalInput;
    private float _jumpInput;
    #endregion

    #region methods
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor")
        {
            _nJumps = 0;
        }
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _myMovementController = GetComponent<CharacterMovementController>();
        _nJumps = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _jumpInput = Input.GetAxis("Jump");


        if(_horizontalInput != 0)
        {
            _myMovementController.SetMovementDirection(_horizontalInput);
        }
        if (_jumpInput != 0)
        {
            if(_nJumps<_limitJumps)
            {
                _myMovementController.JumpRequest();
                _nJumps++;
            }
           
        }
    }
}
