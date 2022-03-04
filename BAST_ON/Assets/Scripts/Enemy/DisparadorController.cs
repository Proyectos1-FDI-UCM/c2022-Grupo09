using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparadorController : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private GameObject _myDisp, _myPlayer, _myEnemy;
    private GameObject _myInstance;
    private Vector3 pos;
    [SerializeField]
    private float frecuencia=6;
    private float timer;
    #endregion


    void Start()
    {

    }

    void Update()
    {
        pos = _myPlayer.transform.position - _myEnemy.transform.position;
        timer += Time.deltaTime;
            if (timer >= frecuencia)
            {
            _myInstance = Instantiate(_myDisp, transform.position, Quaternion.identity);
            _myInstance.transform.Rotate(pos);
            timer = 0;
            }
            
    }
}
