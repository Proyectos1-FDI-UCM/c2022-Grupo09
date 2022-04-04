using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSwapper : MonoBehaviour
{
    private Sprite defaultSprite;
    [SerializeField] private Sprite otherSprite;
    private SpriteRenderer mySpriteRenderer;

    private bool hasBeenSwapped = false;
    
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        defaultSprite = mySpriteRenderer.sprite; 
    }

    public void SwapBG(){
        if(!hasBeenSwapped)
        {
            mySpriteRenderer.sprite = otherSprite;
        }
        else
        {
            mySpriteRenderer.sprite = defaultSprite;
        }    
    }
}
