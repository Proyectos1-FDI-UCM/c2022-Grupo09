using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToNextLevel : MonoBehaviour
{
    #region parameters
    [SerializeField] private string _nextLevelName = "Lvl 2";
    [SerializeField] private GameObject _currentLevelGameObject;
    #endregion


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character_HealthManager player = collision.GetComponent<Character_HealthManager>();
        if (player != null)
        {
            PlayerPrefs.DeleteAll();

            DontDestroyOnLoad(_currentLevelGameObject);
            SceneManager.LoadScene(_nextLevelName);
            StartCoroutine(PausaCarga());
        }
    }
    IEnumerator PausaCarga()
    {
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == _nextLevelName);
        UI_Manager test = GameObject.Find("UI").GetComponent<UI_Manager>();
        yield return new WaitUntil(() => test.GetStarted());
        test.StartGame();
        Destroy(_currentLevelGameObject);
    }
}
