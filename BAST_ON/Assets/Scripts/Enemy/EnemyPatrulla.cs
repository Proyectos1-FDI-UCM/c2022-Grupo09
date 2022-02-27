using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrulla : MonoBehaviour
{
    [SerializeField]
    public float speed;
    private bool rightMov = true;
    public Transform floorDetect;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D floorData = Physics2D.Raycast(floorDetect.position,Vector2.down,1f);
        if (!floorData.collider)
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

        }
    }
}
