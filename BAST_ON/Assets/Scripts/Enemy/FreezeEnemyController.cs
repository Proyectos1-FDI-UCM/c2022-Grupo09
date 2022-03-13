using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeEnemyController : MonoBehaviour
{
    #region references
    [SerializeField]
    Transform _enemyTransform;

    #endregion
    #region properties
    private int _speednull = 0;
    #endregion
    #region methods

    public void FreezeEnemy()
    {
        Vector3 _currentPosition = _enemyTransform.position;
        transform.Translate(_currentPosition * _speednull * Time.deltaTime);
    }


    #endregion
    public void Start()
    {
        _enemyTransform= GetComponent<Transform>();
       
    }


}