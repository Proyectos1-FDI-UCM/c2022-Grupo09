using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region properties
    private bool _activeDragon = false;
    private bool _activeKiwi = false;
    private float _kiwiSlowDown = 0f;
    #endregion

    #region references
    private CharacterMovementController _myCharacterMovementController;

    private CharacterAttackController _myCharacterAttackController;

    private List<EnemyLifeComponent> _listOfEnemies;

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
  
    public void RestartGame()
    {
        SceneManager.LoadScene("Lvl 1");
    }
    public void NewGame()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("Lvl 1");
        StartCoroutine(LoadLevel1());
    }
    IEnumerator LoadLevel1()
    {
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Lvl 1");
        UI_Manager test = GameObject.Find("UI").GetComponent<UI_Manager>();
        //Debug.Log(test);
        yield return new WaitUntil(() => test.GetStarted());
        test.StartGame();
        Destroy(gameObject);
    }
    public void OnPlayerDeath()
    {
        _UIManagerReference.OpenDeathMenu();
    }
    public void SendEnemyLifeComponent(EnemyLifeComponent reference) //A�adimos referencias a EnemyLifeCOmponent a la lista
    {
        _listOfEnemies.Add(reference);
    }
    public void OnEnemyDies(EnemyLifeComponent enemy)
    {
        _listOfEnemies.Remove(enemy);
    }
    public void KiwiCallBack(int duration, float slowDown)
    {
        StartCoroutine(KiwiPowerUp(duration, slowDown));
    }
    IEnumerator KiwiPowerUp(int duration, float slowDown)
    {
        /*foreach (EnemyLifeComponent enemy in _listOfEnemies) //Ralentizar a los enemigos
        {
            if (enemy != null)
            {
                EnemyPatrulla patrulla = enemy.GetComponent<EnemyPatrulla>();
                if (patrulla != null) patrulla.SlowDown(slowDown);
            }
        }*/
        _kiwiSlowDown = slowDown;
        _activeKiwi = true;
        _UIManagerReference.KiwiActive(true);

        yield return new WaitForSeconds(duration/2);
        _UIManagerReference.StartBlinkKiwi();
        yield return new WaitForSeconds(duration/2);
        _activeKiwi = false;
        /*foreach (EnemyLifeComponent enemy in _listOfEnemies) //Reacelerar a todos los enemigos
        {
            if (enemy != null)
            {
                EnemyPatrulla patrulla = enemy.GetComponent<EnemyPatrulla>();
                if (patrulla != null) patrulla.RestoreSpeed();
            }
        }*/
        
        _UIManagerReference.KiwiActive(false);
        //_myCharacterMovementController.NormalVelocity();
    }
    public bool GetKiwiActive()
    {
        return _activeKiwi;
    }
    public float GetKiwiSlowDown()
    {
        return _kiwiSlowDown;
    }
    public void DragonCallBack(float duration, float newImpulse)
    {
        StartCoroutine(DragonPowerUp(duration, newImpulse));
    }
    IEnumerator DragonPowerUp(float duration, float newImpulse)
    {
        _activeDragon = true;
        _UIManagerReference.DragonActive(true);
        _myCharacterMovementController.IncreaseBastonImpulse(newImpulse);
        yield return new WaitForSeconds(duration/2);
        _UIManagerReference.StartBlinkDragon();
        yield return new WaitForSeconds(duration/2);
        _UIManagerReference.DragonActive(false);
        _myCharacterMovementController.DecreaseBastonImpulse(newImpulse);
        _myCharacterAttackController.DecreaseStrenght(newImpulse);
        _activeDragon = false;
    }

    public bool GetDragonActive()
    {
        return _activeDragon;
    }

    public void CompleteGame()
    {
        ExitToMainMenu();
    }
    #endregion

    private void Awake() {
        _listOfEnemies = new List<EnemyLifeComponent>();
        _instance = this;
        _UIManagerReference = _UIReference.GetComponent<UI_Manager>();
        _myCharacterMovementController = _playerReference.GetComponent<CharacterMovementController>();
        _myCharacterAttackController = _playerReference.GetComponent<CharacterAttackController>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _playerReference.SetActive(false);
        Time.timeScale = 0;
    }
}
