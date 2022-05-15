using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStrikingForceController : MonoBehaviour
{
    #region parameters
    [SerializeField] private float conversionValue = 1.0f;
    [SerializeField] private float _impulseStopTime = 3.0f;
    #endregion

    #region properties
    private bool hasBeenStruck = false;
    private float _impulsedElapsedTime = 0;
    #endregion

    #region references
    private MonoBehaviour _myEnemyPatrulla;
    private EnemyLifeComponent _myEnemyLifeComponent;

    private Animator _myAnimator;
    private Transform _myTransform;
    private Rigidbody2D _myRigidBody;
    #endregion

    #region methods
    public void StrikeCallback(Vector3 strikeVector){
        _myEnemyPatrulla.enabled = false;
        _myRigidBody.WakeUp();
        
        _myRigidBody.AddForce(strikeVector, ForceMode2D.Impulse);
        if (_myEnemyPatrulla != null) hasBeenStruck = true;
        _myAnimator.SetBool("haSidoGolpeado", true);

        _impulsedElapsedTime = 0;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        //Debug.Log("la colisión se está detectando");

        CharacterAttackController check = other.gameObject.GetComponent<CharacterAttackController>();

        if(hasBeenStruck && check == null)
        {
            //Debug.Log("tal");
            _myEnemyLifeComponent.ChangeHealth(/*- Mathf.RoundToInt(_myRigidBody.velocity.magnitude / conversionValue)*/ -1);
            
            hasBeenStruck = false;
            _myAnimator.SetBool("haSidoGolpeado", false);
            if (_myEnemyPatrulla != null) _myEnemyPatrulla.enabled = true;
        }
    }

    /// <summary>
    /// Cancela el impulso del bastón y recupera la ruta
    /// </summary>
    private void RestartEnemy()
    {
        _myRigidBody.velocity = Vector3.zero;
        hasBeenStruck = false;
        _myAnimator.SetBool("haSidoGolpeado", false);
        if(_myEnemyPatrulla != null) _myEnemyPatrulla.enabled = true;
    }
    #endregion

    private void Start() 
    {
        _myAnimator = GetComponent<Animator>();
        _myEnemyLifeComponent = GetComponent<EnemyLifeComponent>();
        
        _myEnemyPatrulla = GetComponent<EnemyPatrulla>();
        if(_myEnemyPatrulla == null)
        {
            _myEnemyPatrulla = GetComponent<EnemyPasivo>();
        }

        _myRigidBody = GetComponent<Rigidbody2D>();
        _myTransform = transform;
    }

    private void Update()
    {
        if (hasBeenStruck && _impulsedElapsedTime > _impulseStopTime)
        {
            RestartEnemy();
        }
        else if (hasBeenStruck) _impulsedElapsedTime += Time.deltaTime;
    }
}