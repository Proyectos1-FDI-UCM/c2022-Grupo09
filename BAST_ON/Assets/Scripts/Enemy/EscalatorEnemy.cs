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
    private GameObject _limIzq, _limDer, _limUpIz, _limUpDer;

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
        else if (transform.position.x == _limDer.transform.position.x)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            
            if (transform.position.y <= _limUpDer.transform.position.y)
            {
                transform.eulerAngles = new Vector3(0, 0, -90);
            }
            else if (transform.position.y >= _limUpDer.transform.position.y)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        else if (transform.position.x == _limIzq.transform.position.x)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);

            if (transform.position.y <= _limUpIz.transform.position.y)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (transform.position.y >= _limUpIz.transform.position.y)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
            }

        }
    }
}
