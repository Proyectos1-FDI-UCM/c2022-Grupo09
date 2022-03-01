using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparadorController : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private GameObject _myDisp, _myPlayer, _myEnemy;
    private GameObject _myInstance;
    private Vector2 pos;
    private float timer;
    #endregion


    void Start()
    {

    }

    void Update()
    {
        Vector3 relativePos = _myPlayer.transform.position - transform.position;
        timer += Time.deltaTime;
            if (timer >= 6)
            {
                _myInstance = Instantiate(_myDisp, transform.position, Quaternion.LookRotation(relativePos, Vector3.up));
                timer=0;
            }
    }
}
