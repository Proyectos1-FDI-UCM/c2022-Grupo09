using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiwiController : MonoBehaviour
{
    #region parameters

    [SerializeField]
    private float _SlowDown = 0.25f; //Velocidad de relentización de tiempo
    [SerializeField]
    private float _moreVelocity = 2f;
    [SerializeField]
    private float _duration = 10f;

    private float _currentDuration;

    private bool _isKiwiTime = false;

    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterMovementController check = collision.gameObject.GetComponent<CharacterMovementController>();
        if (check != null && _isKiwiTime == false)
        {
            _isKiwiTime = true;
            _currentDuration = _duration;
            _currentDuration --;

            if (_currentDuration > 0 && _isKiwiTime == true)
            {
                check.PlusVelocity(_moreVelocity);
                Time.timeScale = _SlowDown; //Relentiza el tiempo ssegun el valor del parámetro

            }
            else if (_currentDuration <= 0) 
            {
                _isKiwiTime = false;
                Time.timeScale = 1f;
                Destroy(this.gameObject);
            }
            _currentDuration = 0;
        }
    }
   

    #endregion

    



}
