using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region references

    private Character_HealthManager _myCharacterHealthManager;

    private CharacterMovementController _myCharacterMovementController;

    private EnemyLifeComponent _myEnemyLifeComponent;

    private EnemyPatrulla _myEnemy;

    [SerializeField] private List<EnemyLifeComponent> _listOfEnemies;

    [SerializeField] private GameObject _dragon, _kiwi;


    private UI_Manager _UIManagerReference;

    static private GameManager _instance;

    static public GameManager Instance => _instance;
    
    [SerializeField] private GameObject _playerReference; 
    [SerializeField] private GameObject _UIReference;
    



    #endregion

    #region methods


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
    public void KiwiCallBack(int duration)
    {
        StartCoroutine(CosaDeRalentizar(duration));
    }
    public void SendEnemyLifeComponent(EnemyLifeComponent reference) //Añadimos referencias a EnemyLifeCOmponent a la lista
    {
        _listOfEnemies.Add(reference);
    }
    IEnumerator CosaDeRalentizar(int duration)
    {
        foreach (EnemyLifeComponent enemy in _listOfEnemies) //Relentizar a los enemigos
        {
            EnemyPatrulla patrulla = enemy.GetComponent<EnemyPatrulla>();
            if (patrulla!=null) patrulla.SlowDown(2);
        }
        
        _UIManagerReference.KiwiActive(true);
        yield return new WaitForSeconds(duration);
        foreach (EnemyLifeComponent enemy in _listOfEnemies) //Reacelerar a todos los enemigos
        {
            EnemyPatrulla patrulla = enemy.GetComponent<EnemyPatrulla>();
            if (patrulla != null) patrulla.RestoreSpeed();
        }
        _UIManagerReference.KiwiActive(false);
        _myCharacterMovementController.NormalVelocity();

    }
    public void AvisoDragon(float duration)
    {
        _UIManagerReference.DragonActive(duration);
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
        _playerReference.SetActive(false);
        Time.timeScale = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
