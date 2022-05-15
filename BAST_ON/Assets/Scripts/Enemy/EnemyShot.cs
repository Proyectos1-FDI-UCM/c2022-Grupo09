using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    #region references
    private Transform _myTransform;
    #endregion

    #region parameters
    [SerializeField]
    private float _speed = 1f;
    #endregion

    #region properties
    private float _originalSpeed;
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
        if (baston == null) 
        {
            Destroy(gameObject); 
        }

        EnemyLifeComponent enemy = collision.gameObject.GetComponent<EnemyLifeComponent>();
        if (enemy != null)
        {
            enemy.ChangeHealth(-1);
        }
    }


    public void CambiaRotacion(float rotation)
    {
        _myTransform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    private void SlowDown(float slowDown)
    {
        _speed = _originalSpeed / slowDown;
    }
    #endregion

    private void Start()
    {
        _myTransform = transform;
        _originalSpeed = _speed;
    }

    // Update is called once per frame
    void Update()
    {
        // Comprobación de si hay un Kiwi Activo
        if (GameManager.Instance.GetKiwiActive()) SlowDown(GameManager.Instance.GetKiwiSlowDown());
        else _speed = _originalSpeed;
        _myTransform.Translate(Vector3.right * _speed * Time.deltaTime);
    }
}
