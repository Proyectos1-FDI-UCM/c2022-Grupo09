using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparadorController : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private GameObject _myDisp,_myPlayer;
    private Transform _myTransform;
    private GameObject _myInstance;
    [SerializeField]
    private float frecuencia=6;
    private float timer,ang;
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
            ang = Mathf.Acos(pos.x);
            if (pos.y < 0) ang = -ang;
            ang *= 180 / Mathf.PI;
            _myInstance = Instantiate(_myDisp, transform.position, Quaternion.Euler(0,0,ang));
            timer = 0;
        }
            
    }
}
