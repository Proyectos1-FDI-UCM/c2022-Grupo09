using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Manager : MonoBehaviour
{
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
    ///<summary>
    ///Referencia al sprite de la duración del powerUp kiwi
    ///</summary>
    //[SerializeField] private Sprite kiwiPower;
    ///<summary>
    ///Referencia al sprite de la duración del powerUp DragonFruit
    ///</summary>
    //[SerializeField] private Sprite DragonPower;
    [SerializeField] private GameObject _dragon, _kiwi;

    private Image _kiwiSprite, _dragonSprite;

    [SerializeField] private GameObject _mainMenu, _pauseMenu, _controlsMenu, _hud;
    private GameObject _previousMenu;
    [SerializeField] private GameObject PauseFirstButton,ResumeFirstButton,OpcionClosed;
    private bool _isBlinking = true;

    

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
        _isBlinking = true;
        while (_kiwi.activeSelf)
        {
            _kiwiSprite.enabled = false;
            yield return new WaitForSeconds(0.5f);
            _kiwiSprite.enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
        _isBlinking = false;
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
        _isBlinking = true;
        while (_dragon.activeSelf)
        {
            _dragonSprite.enabled = false;
            yield return new WaitForSeconds(0.5f);
            _dragonSprite.enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
        _isBlinking = false;
    }
    public void StartBlinkDragon()
    {
        StartCoroutine(BlinkDragon());
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
        _isBlinking = false;
    }
}
