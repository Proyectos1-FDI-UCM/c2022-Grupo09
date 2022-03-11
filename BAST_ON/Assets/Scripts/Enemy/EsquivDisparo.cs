using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsquivDisparo : MonoBehaviour
{
    #region references
    [SerializeField]
    private GameObject _myShot, _myPrefab;
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
        _myTransform = _myPrefab.transform;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= freq)
        {
            Instantiate(_myShot, transform.position, Quaternion.Euler(_myTransform.rotation.eulerAngles.x, _myTransform.rotation.eulerAngles.y, _myTransform.rotation.eulerAngles.z));
            timer = 0;
        }
    }
}
