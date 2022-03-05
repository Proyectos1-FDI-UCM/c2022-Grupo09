using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLimit : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyPatrulla enemy = collision.gameObject.GetComponent<EnemyPatrulla>();
        if (enemy != null)
        {
            Debug.Log("e");
            enemy.truecol();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
