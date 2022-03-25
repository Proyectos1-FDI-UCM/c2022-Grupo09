using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] private GameObject _mainMenu, _pauseMenu, _controlsMenu, _hud;
    private GameObject _previousMenu;

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


    public void StartGame()
    {
        //_dragon.SetActive(false);
        //_kiwi.SetActive(false);
        _mainMenu.SetActive(false);
        _hud.SetActive(true);
        GameManager.Instance.StartGame();
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
    }
    public void ExitOptionsMenu()
    {
        _controlsMenu.SetActive(false);
        _previousMenu.SetActive(true);
    }
    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }
    IEnumerator KiwiSprite(int duration)
    {
        _kiwi.SetActive(true);
        yield return new WaitForSeconds(duration);
        _kiwi.SetActive(false);
    }
    IEnumerator DragonSprite(float duration)
    {
        _dragon.SetActive(true);
        yield return new WaitForSeconds(duration);
        _dragon.SetActive(false);
    }
    public void KiwiActive(int duration)
    {
        StartCoroutine(KiwiSprite(duration));
    }
    public void DragonActive(float duration)
    {
        StartCoroutine(DragonSprite(duration));
    }

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _mainMenu.SetActive(true);
        _pauseMenu.SetActive(false);
        _controlsMenu.SetActive(false);
        _hud.SetActive(false);
        _kiwi.SetActive(false);
        _dragon.SetActive(false);
      
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
