using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BastonImpulseController : MonoBehaviour
{
    #region references
    [SerializeField]
    GameObject _player;
    CharacterMovementController _characterMovementController;
    Transform _bastonTransform;
    #endregion
    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PowerUpController powerup = collision.GetComponent<PowerUpController>();
        if (powerup == null)
        {
            Vector2 impulseDirection;
            impulseDirection.x = Mathf.Cos((Mathf.PI / 180) * _bastonTransform.rotation.eulerAngles.z);
            impulseDirection.y = Mathf.Sin((Mathf.PI / 180) * _bastonTransform.rotation.eulerAngles.z);



            // Llamada al método impulso a enemigos (con duck typing?)

            EnemyShot disparo = collision.GetComponent<EnemyShot>();
            if (disparo != null) disparo.CambiaRotacion(_bastonTransform.rotation.eulerAngles.z);



            _characterMovementController.ImpulseRequest(-impulseDirection);
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _bastonTransform = transform;
        _characterMovementController = _player.GetComponent<CharacterMovementController>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        EnemyLifeComponent check = collision.gameObject.GetComponent<EnemyLifeComponent>();
        if (check != null)
        {
            check.ChangeHealth(-1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
