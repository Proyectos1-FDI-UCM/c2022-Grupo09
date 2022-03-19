using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chekpoint : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {

            collision.GetComponent<playerrespwn>().ReachedCheckPoint(transform.position.x, transform.position.y);
        }
    }
}
