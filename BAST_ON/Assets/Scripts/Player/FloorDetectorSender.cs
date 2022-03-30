using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDetectorSender : MonoBehaviour
{
    
    private Enemy_ClimberPatrol _patrolReference;
    
    // Start is called before the first frame update
    void Start()
    {
        _patrolReference = this.gameObject.gameObject.GetComponent<Enemy_ClimberPatrol>();
    }

    private void OnCollisionStay2D(Collision2D other) {
        Debug.Log("se está haciendo la colisión");
        if(other.gameObject.CompareTag("Floor"))
        {
            Debug.Log("y se está detectando que es suelo");
            _patrolReference.FloorUpdateReceiver(true);
        }
        else _patrolReference.FloorUpdateReceiver(false);
    }
}
