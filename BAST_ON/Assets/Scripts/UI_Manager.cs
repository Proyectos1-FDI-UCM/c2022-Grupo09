using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Manager : MonoBehaviour
{
    #region references
    ///<summary>
    ///Array que determina la vida en el UI
    ///</summary>
    [SerializeField] private Image[] _fruitArray;
    ///<summary>
    ///Referencia al sprite de la vida llena
    ///</summary>
    [SerializeField] private Sprite fullFruit;
    ///<summary>
    ///Referencia al sprite de la vida vacía
    ///</summary>
    [SerializeField] private Sprite emptyFruit;
    /// <summary>
    /// Referencias al game object de dragon fruit y kiwi del UI
    /// </summary>
    [SerializeField] private GameObject _dragon, _kiwi;
    /// <summary>
    /// Referencias al sprite de dragon fruit y kiwi del UI
    /// </summary>
    private Image _kiwiSprite, _dragonSprite;
    /// <summary>
    /// Referencias a los menús de la UI
    /// </summary>
    [SerializeField] private GameObject _mainMenu, _pauseMenu, _controlsMenu, _hud;
    /// <summary>
    /// Referencia al menú previo al menú de controles cuando se entra en este
    /// </summary>
    private GameObject _previousMenu;
    [SerializeField] private GameObject PauseFirstButton,ResumeFirstButton,OpcionClosed;
    #endregion


    #region methods
    public void updateLifeBar(int _currentHealth){
        for(int i = 0; i < _currentHealth; i++)
        {
            _fruitArray[i].sprite = fullFruit;
        }
        for(int i = _currentHealth; i < _fruitArray.Length; i++)
        {
            _fruitArray[i].sprite = emptyFruit;
            
        }
    }

   
    public void KiwiActive(bool active)
    {
        _kiwi.SetActive(active);
        _kiwiSprite.enabled = active;
    }
    IEnumerator BlinkKiwi()
    {
        while (_kiwi.activeSelf)
        {
            _kiwiSprite.enabled = false;
            yield return new WaitForSeconds(0.5f);
            _kiwiSprite.enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
    }
   
    public void StartBlinkKiwi()
    {
        StartCoroutine(BlinkKiwi());
    }
    public void DragonActive(bool active)
    {
        _dragon.SetActive(active);
        _dragonSprite.enabled = active;
    }

    IEnumerator BlinkDragon()
    {
        while (_dragon.activeSelf)
        {
            _dragonSprite.enabled = false;
            yield return new WaitForSeconds(0.5f);
            _dragonSprite.enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void StartBlinkDragon()
    {
        StartCoroutine(BlinkDragon());
    }
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        _mainMenu.SetActive(false);
        _hud.SetActive(true);
        GameManager.Instance.StartGame();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(PauseFirstButton);
    }
    public void StartGame()
    {
        _mainMenu.SetActive(false);
        _hud.SetActive(true);
        GameManager.Instance.StartGame();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(PauseFirstButton);
    }
    public void ResumeGame()
    {
        _pauseMenu.SetActive(false);
        GameManager.Instance.Resume();
    }
    public void PauseGame()
    {
        if (!_mainMenu.activeSelf && !_controlsMenu.activeSelf)
        {
            GameManager.Instance.Pause();
            _pauseMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(ResumeFirstButton);
        }
    }
    public void ExitToMainMenu()
    {
        GameManager.Instance.ExitToMainMenu();
    }
    public void OpenOptionsMenu([SerializeField] GameObject previousMenu)
    {
        _previousMenu = previousMenu;
        _previousMenu.SetActive(false);
        _controlsMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(OpcionClosed);
    }
    public void ExitOptionsMenu()
    {
        _controlsMenu.SetActive(false);
        _previousMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(ResumeFirstButton);
    }
    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _mainMenu.SetActive(true);
        _pauseMenu.SetActive(false);
        _controlsMenu.SetActive(false);
        _hud.SetActive(false);
        _dragon.SetActive(false);
        _kiwi.SetActive(false);
        _kiwiSprite = _kiwi.GetComponent<Image>();
        _dragonSprite = _dragon.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7)) 
        {
            if (!_pauseMenu.activeSelf) PauseGame();
            else ResumeGame();
        }
    }
}
