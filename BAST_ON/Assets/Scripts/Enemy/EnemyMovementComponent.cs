using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementComponent : MonoBehaviour
{
    #region parameters
    private Vector3 _movementDirection = Vector3.zero;
    #endregion

    #region references
    Transform _enemyTransform;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _enemyTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}