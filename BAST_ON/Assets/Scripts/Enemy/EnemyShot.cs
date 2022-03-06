using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{

    [SerializeField]
    private float _speed;
    void Start()
    {
      //  objtv = new Vector2(_myPlayer.transform.position.x, _myPlayer.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
       
        transform.Translate(Vector3.right*_speed * Time.deltaTime);
    }
}
