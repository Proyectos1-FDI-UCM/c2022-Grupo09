using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region references

    Character_HealthManager _myCharacterHealthManager;
    #endregion

    #region methods

    public void PickTangerine()
    {

    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _myCharacterHealthManager = GetComponent<Character_HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
