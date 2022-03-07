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
    private GameObject _pauseMenu;
    [SerializeField]
    private GameManager _gameManager;

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

    public void ResumeGame()
    {
        _pauseMenu.SetActive(false);
        _gameManager.Resume();
    }
    public void SetPause()
    {
        _pauseMenu.SetActive(true);
        _gameManager.Pause();
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPause();
        }
    }
}
