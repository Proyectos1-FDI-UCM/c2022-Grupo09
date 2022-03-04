using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparadorController : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private GameObject _myDisp;
    private GameObject _myInstance;
    [SerializeField]
    private float frecuencia=6;
    private float timer;
    #endregion


    void Start()
    {

    }

    void Update()
    {
        timer += Time.deltaTime;
            if (timer >= frecuencia)
            {
            _myInstance = Instantiate(_myDisp, transform.position, Quaternion.identity);
            timer = 0;
            }
            
    }
}
