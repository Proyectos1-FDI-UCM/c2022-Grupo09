using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalJuego : MonoBehaviour
{
    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character_HealthManager player = collision.GetComponent<Character_HealthManager>();
        if (player != null)
        {
            GameManager.Instance.CompleteGame();
        }
    }
    #endregion
}
