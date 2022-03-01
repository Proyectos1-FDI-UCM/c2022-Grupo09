using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    [SerializeField]
    private float speed, tiempo;
    private float cont;

    // Update is called once per frame
    void Update()
    {
        cont += Time.deltaTime;
        if (cont <= tiempo) transform.Translate(Vector2.right * speed * Time.deltaTime);
        else if (cont > tiempo && cont < tiempo * 2)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else cont = 0;

    }
}
