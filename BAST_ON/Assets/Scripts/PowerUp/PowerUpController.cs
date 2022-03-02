using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    #region references

    Character_HealthManager _myCharacter_HealthManager;
    private int _extraHealth = 1;


    #endregion
    #region methods
    //Método para destruir el powerup una vez es recogido por el jugador mediante TriggerEnter
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _myCharacter_HealthManager.ChangeHealthValue(_extraHealth);
            Destroy(gameObject);
        }
    }
    #endregion
    void Start()
    {
        _myCharacter_HealthManager = GetComponent<Character_HealthManager>();
    }
}
