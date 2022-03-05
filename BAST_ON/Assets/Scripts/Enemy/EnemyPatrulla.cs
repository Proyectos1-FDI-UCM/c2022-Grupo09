using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrulla : MonoBehaviour
{
    [SerializeField]
    public float speed;
    [SerializeField]
    private bool rightMov = true;
    [SerializeField]
    private GameObject _limIzq, _limDer;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (transform.position.x >= _limDer.transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            rightMov = false;
        }
        else if (transform.position.x <= _limIzq.transform.position.x)

        {

            transform.eulerAngles = new Vector3(0, 0, 0);
            rightMov = true;
        }
    }

}
