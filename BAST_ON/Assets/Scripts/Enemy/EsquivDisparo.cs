using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsquivDisparo : MonoBehaviour
{
    #region references
    [SerializeField]
    private GameObject _myShot;
    private Transform _myTransform;
    #endregion
    #region parameters
    [SerializeField]
    private float freq=2;
    #endregion
    #region properties
    private float timer;
    #endregion 
    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= freq)
        {
            Instantiate(_myShot, _myTransform.position, Quaternion.Euler(_myTransform.rotation.eulerAngles));
            timer = 0;
        }
    }
}
