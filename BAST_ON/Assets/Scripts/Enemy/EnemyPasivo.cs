using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPasivo : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float speed = 3, distance = 1;
    private bool rotate = true;
    #endregion

    #region references
    [SerializeField]
    private Transform _detector;
    private Rigidbody2D _myRigidBody;
    private RaycastHit2D _groundDetect, _wallDetect;
    #endregion

    #region properties
    private Vector2 _movementDirection;
    #endregion

    #region methods

    #endregion


    private void Start()
    {
        _myRigidBody = GetComponent<Rigidbody2D>();
        _movementDirection = Vector2.right;
    }

    private void Update()
    {
        _myRigidBody.MovePosition(_movementDirection);
        _groundDetect = Physics2D.Raycast(_detector.position, Vector2.down, distance);
        _wallDetect = Physics2D.Raycast(_detector.position, _movementDirection, distance);

        if (!_groundDetect.collider || _wallDetect.collider)
        {
            if (rotate)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                rotate = !rotate;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                rotate = !rotate;
            }
        }

    }


}
