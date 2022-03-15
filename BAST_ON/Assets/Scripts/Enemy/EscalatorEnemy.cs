using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscalatorEnemy : MonoBehaviour
{
    [SerializeField]
    public float speed;
    [SerializeField]
    private bool rightMov = true;
    [SerializeField]
    private GameObject _limIzq, _limDer, _limUpLeft, _limUpRight;

    void Update()
    {

        if (rightMov)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.position.x >= _limDer.transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
            else if (transform.position.x <= _limIzq.transform.position.x)

            {

                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        else if (!rightMov)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            if (transform.position.x <= _limIzq.transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
            else if (transform.position.x >= _limDer.transform.position.x)
            {

                transform.eulerAngles = new Vector3(0, 0, 0);
            }


        }
    }
}
