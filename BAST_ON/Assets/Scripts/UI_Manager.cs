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
    [SerializeField]
    private GameObject _mainMenu, _pauseMenu, _optionsMenu;
    private GameObject _previusMenu;

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
        _mainMenu.SetActive(false);
        GameManager.Instance.StartGame();
    }
    public void ResumeGame()
    {
        Debug.Log("a");
        _pauseMenu.SetActive(false);
        GameManager.Instance.Resume();
    }
    public void PauseGame()
    {
        _pauseMenu.SetActive(true);
    }
    public void ExitToMainMenu()
    {
        GameManager.Instance.ExitToMainMenu();
    }
    public void OpenOptionsMenu([SerializeField] GameObject previusMenu)
    {
        _previusMenu = previusMenu;
        _previusMenu.SetActive(false);
        _optionsMenu.SetActive(true);
    }
    public void ExitOptionsMenu()
    {
        _optionsMenu.SetActive(false);
        _previusMenu.SetActive(true);
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
        _optionsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
