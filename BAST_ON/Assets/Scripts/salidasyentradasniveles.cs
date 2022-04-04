using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class salidasyentradasniveles : MonoBehaviour
{

    [SerializeField] private GameObject salidanivel;
    private GameObject player;

    [SerializeField] private GameObject[] Backgrounds;
   
       

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character_HealthManager player = collision.GetComponent<Character_HealthManager>();
        
        if(player != null)
        {
            player.transform.position = salidanivel.transform.position;
            foreach(var i in Backgrounds)
            {
                i.GetComponent<BGSwapper>().SwapBG();
            }
        }
    }
}
