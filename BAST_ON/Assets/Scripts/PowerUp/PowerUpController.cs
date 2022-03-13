using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    #region references
    GameObject _myPowerUp;
    #endregion
    #region parameters
    [SerializeField]
    private int _extraHealth = 1; //variable para aumentar 1 pto de vida
    #endregion
    #region methods
    //Método para destruir el powerup una vez es recogido por el jugador mediante TriggerEnter
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character_HealthManager check = collision.GetComponent<Character_HealthManager>(); //variable para acceder al characterhealth manager si este colisiona con el jugador
        if (check != null)
        {
            check.ChangeHealthValue(_extraHealth);
            Destroy(_myPowerUp);
        }
    }
    #endregion
    private void Start()
    {
        _myPowerUp = gameObject;
    }

}
