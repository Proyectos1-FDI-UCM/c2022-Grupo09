using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    [SerializeField]
    private GameObject _myPlayer;
    [SerializeField]
    private float _speed;
    private Vector2 objtv;
    private Vector2 rotation;
    void Start()
    {
        objtv = new Vector2(_myPlayer.transform.position.x, _myPlayer.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
       
        transform.position = Vector2.MoveTowards(transform.position, objtv, _speed * Time.deltaTime);
    }
}
