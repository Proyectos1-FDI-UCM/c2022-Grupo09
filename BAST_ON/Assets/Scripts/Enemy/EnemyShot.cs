using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    #region references
    private Transform _myTransform;
    #endregion

    #region references
    [SerializeField]
    private float _speed;
    #endregion


    #region methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Character_HealthManager player = collision.gameObject.GetComponent<Character_HealthManager>();
        if (player != null)
        {
            player.ChangeHealthValue(-1, _myTransform.right);
            Destroy(gameObject);
        }
        BastonImpulseController baston = collision.gameObject.GetComponent<BastonImpulseController>();
        if (baston == null) Destroy(gameObject);

        EnemyLifeComponent enemy = collision.gameObject.GetComponent<EnemyLifeComponent>();
        if (enemy != null)
        {
            enemy.ChangeHealth(-1);
            Destroy(gameObject);
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
