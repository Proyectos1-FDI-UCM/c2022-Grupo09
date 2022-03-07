using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStrikingForceController : MonoBehaviour
{

    private EnemyPatrulla _myEnemyPatrulla;
    private EnemyLifeComponent _myEnemyLifeComponent;

    private Transform _myTransform;
    private Rigidbody _myRigidBody;

    private Vector3 _strikeDirection = Vector3.zero; 

    private bool hasBeenStruck = false;
    public void StrikeCallback(Vector3 strikeVector){
        _myEnemyPatrulla.enabled = false;
        _strikeDirection = strikeVector;
        hasBeenStruck = true;
    }

    private void OnCollisionEnter(Collision other) {
        if(hasBeenStruck){
            Destroy(gameObject);
        }
    }    

    private void Update() {
        if(hasBeenStruck){
            _myTransform.Translate(_strikeDirection * Time.deltaTime);
        }
    }

    private void Start() {
        _myEnemyLifeComponent = this.gameObject.GetComponent<EnemyLifeComponent>();
        _myEnemyPatrulla = gameObject.GetComponent<EnemyPatrulla>();
        _myTransform = gameObject.transform;
    }    
}
