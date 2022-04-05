using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSwapper : MonoBehaviour
{
    [SerializeField] GameObject _JungleBackground;
    [SerializeField] GameObject _FactoryBackground;
    

    private bool hasBeenSwapped = false;
    
    void Start()
    {
        _JungleBackground.SetActive(true);
        _FactoryBackground.SetActive(false);
    }

    public void SwapBG(){
        if(!hasBeenSwapped)
        {
            _FactoryBackground.SetActive(true);
            _JungleBackground.SetActive(false);
        }
        else
        {
            _JungleBackground.SetActive(true);
            _FactoryBackground.SetActive(false);
        }    
    }
}
