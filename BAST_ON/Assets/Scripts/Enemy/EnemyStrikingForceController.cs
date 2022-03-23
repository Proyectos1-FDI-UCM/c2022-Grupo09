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
    private EnemyPatrulla _myEnemyPatrulla;
    private EnemyLifeComponent _myEnemyLifeComponent;

    private Animator _myAnimator;
    private Transform _myTransform;
    private Rigidbody2D _myRigidBody;
    #endregion

    #region methods
    public void StrikeCallback(Vector3 strikeVector){
        _myEnemyPatrulla.enabled = false;
        _myRigidBody.WakeUp();
        Debug.Log(_myRigidBody.IsAwake());
        _myRigidBody.AddForce(strikeVector, ForceMode2D.Impulse);
        hasBeenStruck = true;
        _myAnimator.SetBool("haSidoGolpeado", true);

        _impulsedElapsedTime = 0;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        //Debug.Log("la colisión se está detectando");

        CharacterAttackController check = other.gameObject.GetComponent<CharacterAttackController>();

        if(hasBeenStruck && check == null)
        {
            Debug.Log("hey");
            _myEnemyLifeComponent.ChangeHealth(/*- Mathf.RoundToInt(_myRigidBody.velocity.magnitude / conversionValue)*/ -1);
            _myRigidBody.Sleep();
            hasBeenStruck = false;
            _myAnimator.SetBool("haSidoGolpeado", false);
            _myEnemyPatrulla.enabled = true;
        }
    }

    /// <summary>
    /// Cancela el impulso del bastón y llama al método de recuperar ruta
    /// </summary>
    private void RestartEnemy()
    {
        _myRigidBody.velocity = Vector3.zero;
        hasBeenStruck = false;
        _myAnimator.SetBool("haSidoGolpeado", false);

        // Llamar aquí al método de restart ruta y que al llegar al sitio reactive el comp de patrulla
        // ¿Que ese método ha una cosa u otra dependiendo del enemigo con un enum?
    }
    #endregion

    private void Start() 
    {
        _myAnimator = GetComponent<Animator>();
        _myEnemyLifeComponent = GetComponent<EnemyLifeComponent>();
        _myEnemyPatrulla = GetComponent<EnemyPatrulla>();
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