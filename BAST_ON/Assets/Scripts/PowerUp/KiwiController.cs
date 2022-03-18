using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiwiController : MonoBehaviour
{
    #region references
    
    private GameManager _myGameManager;
    #endregion

    #region parameters

    [SerializeField]
    private float _SlowDown = 0.25f; //Velocidad de relentización de tiempo
    [SerializeField]
    private float _moreVelocity = 2f;
    [SerializeField]
    private float _duration = 10f;

    private float _currentDuration;

    

    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager check = collision.gameObject.GetComponent<GameManager>();
        if (check != null)
        {
                _myGameManager.KiwiCallBack();
            
                Destroy(this.gameObject);
            /*if (_currentDuration > 0 && _isKiwiTime == true)
            {*/
            /*else if (_currentDuration <= 0) 
            {
                //_isKiwiTime = false;
                //Time.timeScale = 1f;
               
            }
            _currentDuration = 0;*/
        }
    }


    #endregion

    private void Start()
    {
        _myGameManager = GameManager.Instance;

    }



}
