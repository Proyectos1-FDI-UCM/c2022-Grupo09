using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chekpoint : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerrespwn player = collision.GetComponent<playerrespwn>();



        if (player!=null)
        {
          player.ReachedCheckPoint(transform.position.x, transform.position.y);
        }
    }
}
