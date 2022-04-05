﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPasivo : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float speed = 1, distance = 1;
    private bool rotate = true;
    private int layerF,layerE;
    #endregion

    #region references
    [SerializeField]
    private Transform _detector;
    private Rigidbody2D _myRigidBody;
    private RaycastHit2D _groundDetect, _wallDetect, _enemyDetect;
    #endregion

    #region properties
    private Vector2 _movementDirection;
    #endregion

    #region methods

    #endregion


    private void Start()
    {
        layerF = LayerMask.GetMask("Floor");
        layerE = LayerMask.GetMask("Enemies");
        _myRigidBody = GetComponent<Rigidbody2D>();
        _movementDirection = Vector2.right;
    }

    private void Update()
    {

        _groundDetect = Physics2D.Raycast(_detector.position, Vector2.down, distance,layerF);
        _wallDetect = Physics2D.Raycast(_detector.position, _movementDirection, distance, layerF);
        _enemyDetect = Physics2D.Raycast(_detector.position, _movementDirection, distance, layerE);

        if (!_groundDetect.collider || _wallDetect.collider||_enemyDetect)
        {
            if (rotate)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                _movementDirection = -_movementDirection;
                rotate = !rotate;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                _movementDirection = -_movementDirection;
                rotate = !rotate;
            }
        }

    }
    private void FixedUpdate()
    {
        _myRigidBody.MovePosition(_myRigidBody.position + _movementDirection.normalized * speed*Time.fixedDeltaTime);
    }


}
