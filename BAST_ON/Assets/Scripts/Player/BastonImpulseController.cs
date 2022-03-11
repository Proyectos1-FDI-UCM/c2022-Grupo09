using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BastonImpulseController : MonoBehaviour
{
    #region references
    [SerializeField]
    GameObject _player;
    CharacterMovementController _characterMovementController;

    CharacterAttackController _myCharacterAttackController;

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

            EnemyShot disparo = collision.GetComponent<EnemyShot>();
            if (disparo != null) disparo.CambiaRotacion(_bastonTransform.rotation.eulerAngles.z);

            _characterMovementController.ImpulseRequest(-impulseDirection);

            //Detectar enemigo con el bastón y empujarlo
            EnemyStrikingForceController enemigo = collision.GetComponent<EnemyStrikingForceController>();
            if (enemigo != null) enemigo.StrikeCallback(impulseDirection * _myCharacterAttackController.RepelStrenght);
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _bastonTransform = transform;
        _characterMovementController = _player.GetComponent<CharacterMovementController>();
        _myCharacterAttackController = _player.GetComponent<CharacterAttackController>();        
    }
    // Update is called once per frame
    void Update()
    {
    }
}
