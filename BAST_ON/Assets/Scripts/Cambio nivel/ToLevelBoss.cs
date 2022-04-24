using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToLevelBoss : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character_HealthManager player = collision.GetComponent<Character_HealthManager>();
        if (player != null)
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("Lvl Joseju");
        }
    }
}
