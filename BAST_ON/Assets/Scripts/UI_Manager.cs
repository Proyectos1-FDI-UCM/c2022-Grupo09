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
        GameManager.Instance.StartGame();
    }
    public void ResumeGame()
    {
        Debug.Log("a");
        GameManager.Instance.Resume();
        _pauseMenu.SetActive(false);
    }
    public void PauseGame()
    {
        _pauseMenu.SetActive(true);
    }
    public void ExitToMainMenu()
    {
        GameManager.Instance.ExitToMainMenu();
    }
    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _mainMenu.SetActive(false);
        _pauseMenu.SetActive(false);
        _optionsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
