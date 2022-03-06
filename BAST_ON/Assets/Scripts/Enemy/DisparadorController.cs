using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparadorController : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private GameObject _myDisp,_myPlayer;
    private GameObject _myInstance;
    [SerializeField]
    private float frecuencia=6;
    private float timer,ang;
    private Vector2 pos;
    #endregion


    void Start()
    {

    }

    void Update()
    {
        timer += Time.deltaTime;
            if (timer >= frecuencia)
            {

            pos = _myPlayer.transform.position - _myDisp.transform.position;
            //pos.Normalize();
            ang = Mathf.Acos(pos.normalized.x);
            pos.y = -pos.y;
            if (pos.y < 0)
            {

                ang = -ang;
            }
            Debug.Log(pos.y);
            ang *= 180 / Mathf.PI;
            _myInstance = Instantiate(_myDisp, transform.position, Quaternion.Euler(0,0,ang));
            // _myInstance=
            timer = 0;
            }
            
    }
}
