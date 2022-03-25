using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFruitController : MonoBehaviour
{
    #region parameters
    ///<summary>
    ///Multiplicador de fuerza del powerup
    ///</summary>
    [SerializeField]
    private float newStrenght = 1.4f; 

    ///<summary>
    ///Duracion del powerup
    ///</summary>
    [SerializeField] private float strenghtDuration = 10.0f;
    #endregion

    #region methods
    //M�todo para destruir el powerup una vez es recogido por el jugador mediante TriggerEnter
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterAttackController check = collision.GetComponent<CharacterAttackController>(); //variable para acceder al characterhealth manager si este colisiona con el jugador
        if (check != null)
        {
            check.IncreaseStrenght(newStrenght);
            GameManager.Instance.DragonCallBack(strenghtDuration, newStrenght);
            Destroy(gameObject);
        }
    }
    #endregion

}

