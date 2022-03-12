using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStrikingForceController : MonoBehaviour
{
    [SerializeField] private float conversionValue = 1.0f;
    private EnemyPatrulla _myEnemyPatrulla;
    private EnemyLifeComponent _myEnemyLifeComponent;
    private Transform _myTransform;
    private Rigidbody2D _myRigidBody;
    private bool hasBeenStruck = false;
    public void StrikeCallback(Vector3 strikeVector){
        _myEnemyPatrulla.enabled = false;
        _myRigidBody.WakeUp();
        Debug.Log(_myRigidBody.IsAwake());
        _myRigidBody.AddForce(strikeVector, ForceMode2D.Impulse);
        hasBeenStruck = true;
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        Debug.Log("la colisión se está detectando");

        CharacterAttackController check = other.gameObject.GetComponent<CharacterAttackController>();

        if(hasBeenStruck && check == null)
        {
            Debug.Log("hey");
            _myEnemyLifeComponent.ChangeHealth(/*- Mathf.RoundToInt(_myRigidBody.velocity.magnitude / conversionValue)*/ -1);
            _myRigidBody.Sleep();
            hasBeenStruck = false;
            _myEnemyPatrulla.enabled = true;
        }
    }
    
    
        private void Start() 
        {
        _myEnemyLifeComponent = GetComponent<EnemyLifeComponent>();
        _myEnemyPatrulla = GetComponent<EnemyPatrulla>();
        _myRigidBody = GetComponent<Rigidbody2D>();
        _myTransform = transform;
        }    
}