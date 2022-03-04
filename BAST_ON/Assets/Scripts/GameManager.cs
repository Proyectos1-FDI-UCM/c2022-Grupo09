using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void PickTangerine()
    {

    }

    public void OnHealthValueChange(int newHealthValue)
    {
        _UIManagerReference.updateLifeBar(newHealthValue);
    }

    public void OnPlayerDeath()
    {
        //@TODO implementar 
    }
    #endregion
    
    private void Awake() {
        _instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _myCharacterHealthManager = _playerReference.GetComponent<Character_HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
