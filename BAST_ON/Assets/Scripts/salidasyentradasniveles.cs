using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class salidasyentradasniveles : MonoBehaviour
{

    [SerializeField] private GameObject salidanivel;
    [SerializeField] private GameObject cam;

    [SerializeField] private bool swapsBG = false;
    private GameObject player;


    private BGSwapper camBGSwapper;
   
    private void Start() {
        camBGSwapper = cam.GetComponent<BGSwapper>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character_HealthManager player = collision.GetComponent<Character_HealthManager>();
        
        if(player != null)
        {
            player.transform.position = salidanivel.transform.position;
            
            if(swapsBG){
            camBGSwapper.SwapBG();
            }
            
        }
    }
}
