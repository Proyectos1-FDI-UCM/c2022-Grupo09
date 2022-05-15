using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiwiController : MonoBehaviour
{
   
    #region parameters
    [SerializeField]
    private float _SlowDown = 10f; //Velocidad de relentizaci�n de tiempo
    [SerializeField]
    private int _duration = 10;
    #endregion


    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterMovementController check = collision.GetComponent<CharacterMovementController>();
        if (check != null && !GameManager.Instance.GetKiwiActive())
        {
            GameManager.Instance.KiwiCallBack(_duration, _SlowDown);
            Destroy(gameObject);
        }
    }
    #endregion
    
}
