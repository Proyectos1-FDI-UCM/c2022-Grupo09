using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparadorController : MonoBehaviour
{
    #region references
    [SerializeField]
    private GameObject _myDisp, _myPlayer;
    private Transform _myTransform;
    #endregion

    #region parameters
    [SerializeField]
    private float frecuencia = 6;
    #endregion

    #region properties
    private float timer, ang;
    private Vector2 pos;
    #endregion


    void Start()
    {
        _myTransform = transform;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= frecuencia)
        {
            pos = _myPlayer.transform.position - _myTransform.position;
            pos.Normalize();
            ang = Mathf.Acos(pos.x);
            if (pos.y < 0) ang = -ang;
            ang *= 180 / Mathf.PI;
            Instantiate(_myDisp, _myTransform.position, Quaternion.Euler(0, 0, ang));
            timer = 0;
        }
            
    }
}
