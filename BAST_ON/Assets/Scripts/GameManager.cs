using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region references

    private Character_HealthManager _myCharacterHealthManager;

    private UI_Manager _UIManagerReference;

    static private GameManager _instance;

    static public GameManager Instance => _instance;
    
    [SerializeField] private GameObject _playerReference; 
    [SerializeField] private GameObject _UIReference;
    #endregion

    #region methods
    public void Pause()
    {
        _UIManagerReference.PauseGame();
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }
    public void ExitToMainMenu()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnHealthValueChange(int newHealthValue)
    {
        _UIManagerReference.updateLifeBar(newHealthValue);
    }
    public void StartGame()
    {

    }


    public void OnPlayerDeath()
    {
        //@TODO implementar 
    }
    #endregion
    
    private void Awake() {
        _instance = this;
        _UIManagerReference = _UIReference.GetComponent<UI_Manager>();
        _myCharacterHealthManager = _playerReference.GetComponent<Character_HealthManager>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
