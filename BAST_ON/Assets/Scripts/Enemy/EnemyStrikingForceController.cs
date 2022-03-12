using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStrikingForceController : MonoBehaviour
{
    [SerializeField] private float conversionValue = 5.0f;
    private EnemyPatrulla _myEnemyPatrulla;
    private EnemyLifeComponent _myEnemyLifeComponent;
    private Transform _myTransform;
    private Rigidbody2D _myRigidBody;
    private bool hasBeenStruck = false;
    public void StrikeCallback(Vector3 strikeVector){
        _myEnemyPatrulla.enabled = false;
        _myRigidBody.WakeUp();
        _myRigidBody.AddForce(strikeVector, ForceMode2D.Impulse);
        hasBeenStruck = true;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(hasBeenStruck){
            _myEnemyLifeComponent.ChangeHealth(Mathf.RoundToInt(_myRigidBody.velocity.magnitude / conversionValue));
            _myRigidBody.Sleep();
            hasBeenStruck = false;

        }
    } 
    
    private void Start() {
        _myEnemyPatrulla = GetComponent<EnemyPatrulla>();
        _myRigidBody = GetComponent<Rigidbody2D>();
        _myTransform = transform;
    }    
}
