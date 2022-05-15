using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToLevel2 : MonoBehaviour
{
    #region parameters
    [SerializeField] private string _levelName = "Lvl 2";
    [SerializeField] private GameObject _currentLevel;
    #endregion

    #region properties
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character_HealthManager player = collision.GetComponent<Character_HealthManager>();
        if (player != null)
        {
            PlayerPrefs.DeleteAll();
            
            DontDestroyOnLoad(_currentLevel);
            SceneManager.LoadScene(_levelName);
            StartCoroutine(PausaCarga());
        }
    }
    IEnumerator PausaCarga()
    {
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Lvl 2");
        UI_Manager test = GameObject.Find("UI").GetComponent<UI_Manager>();
        yield return new WaitUntil(() => test.GetStarted());
        test.StartGame();
        Destroy(_currentLevel);
    }
}
