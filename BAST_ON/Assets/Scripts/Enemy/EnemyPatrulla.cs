using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrulla : MonoBehaviour
{
    [SerializeField]
    public float speed;
    private bool col = false;
    [SerializeField]
    private bool rightMov = true;

       public void truecol()
    {
        col = true;
    }

        void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        if (col)
        {
            if (rightMov)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                rightMov = false;
            }
            else

            {
    
                transform.eulerAngles = new Vector3(0, 0, 0);
                rightMov = true;
            }
            col = false;
        }
    }
}
