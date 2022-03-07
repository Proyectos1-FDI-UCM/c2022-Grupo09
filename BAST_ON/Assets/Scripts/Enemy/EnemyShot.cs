using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    #region references
    private Transform _myTransform;
    #endregion


    [SerializeField]
    private float _speed;


    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character_HealthManager player = collision.GetComponent<Character_HealthManager>();
        if (player != null)
        {
            player.ChangeHealthValue(-1);
            Destroy(gameObject);
        }
        else
        {
            BastonImpulseController baston = collision.GetComponent<BastonImpulseController>();
            if (baston == null) Destroy(gameObject);
        }
    }

    public void CambiaRotacion(float rotation)
    {
        _myTransform.rotation = Quaternion.identity;
        _myTransform.Rotate(Vector3.forward, rotation);
    }
    #endregion

    private void Start()
    {
        _myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }
}
