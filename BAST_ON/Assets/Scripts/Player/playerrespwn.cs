using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerrespwn : MonoBehaviour
{
    private float chekpointpositionx, chekpointpositiony;



    void Start()
    {
        if(PlayerPrefs.GetFloat("chekpointpositionx") !=0)
        {
            transform.position = (new Vector2(PlayerPrefs.GetFloat("chekpointpositionx"), PlayerPrefs.GetFloat("chekpointpositiony")));

        }

    }

    public void ReachedCheckPoint(float x, float y)
    {
        PlayerPrefs.SetFloat("chekpointpositionx", x);
        PlayerPrefs.SetFloat("chekpointpositiony", y);

    }


}


