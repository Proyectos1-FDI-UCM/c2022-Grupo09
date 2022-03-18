using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region references

    private Character_HealthManager _myCharacterHealthManager;

    private EnemyLifeComponent _myEnemyLifeComponent;

    [SerializeField] private List<EnemyLifeComponent> _listOfEnemies;

    

    private UI_Manager _UIManagerReference;

    static private GameManager _instance;

    static public GameManager Instance => _instance;
    
    [SerializeField] private GameObject _playerReference; 
    [SerializeField] private GameObject _UIReference;


    #endregion

    #region methods

    public void SendEnemyLifeComponent(EnemyLifeComponent reference) 
    {
        _listOfEnemies.Add(reference);
        
    
    }
    public void Pause()
    {
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
        _playerReference.SetActive(true);
        Time.timeScale = 1;
    }
  
    public void OnPlayerDeath()
    {
        ExitToMainMenu(); 
    }

    public void KiwiCallback() 
    {
        foreach (var enemy in _listOfEnemies) 
        {
            StartCoroutine(CosaDeRalentizar(EnemyLifeComponent, 5));
        }

    }


    IEnumerator CosaDeRalentizar(int duration) {
        //ralentizar
        yield return new WaitForSeconds(duration);
        //desralentizar
    }
    #endregion
    
    private void Awake() {

        _listOfEnemies = new List<EnemyLifeComponent>();
        _instance = this;
        _UIManagerReference = _UIReference.GetComponent<UI_Manager>();
        _myCharacterHealthManager = _playerReference.GetComponent<Character_HealthManager>();
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _myEnemyLifeComponent = GetComponent<EnemyLifeComponent>();
        _playerReference.SetActive(false);
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
