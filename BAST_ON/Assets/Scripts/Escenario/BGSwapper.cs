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
        SwapBG(false);
    }

    

    public void SwapBG(bool swapValue)
    {
        _JungleBackground.SetActive(!swapValue);
        _FactoryBackground.SetActive(swapValue);
        hasBeenSwapped = swapValue;   
    }
}
