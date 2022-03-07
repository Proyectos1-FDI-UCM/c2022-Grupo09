using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeComponent : MonoBehaviour
{
    #region references 
    [SerializeField]
    private int _currentHealth = 3;

    [SerializeField]
    private int _maxHealth = 3;

    [SerializeField]
    private GameObject _myEnemy;


    ///<summary>
    ///Valor que detecta si ha sido golpeado
    ///</summary>
    private bool _hasBeenHit = false;

    private Transform _thisTransform;
    [SerializeField]
    private GameObject _myPowerUp;

    

    private EnemyStrikingForceController _myEnemyStrikingForceController;
    #endregion

    #region methods

    public void ChangeHealth(int value)
    {
        _currentHealth += value;

        if (_currentHealth <= 0)
        {
            ReleasePowerUp();
            Die();
           
        }
        if (_currentHealth > _maxHealth)
        {
            //Hay que mantener el tope de vida
            _currentHealth = _maxHealth;
        }
    }

    public void Die()
    {
        Destroy(_myEnemy);
    }

    public Vector3 referenciaAlTransformPos(){
         return _thisTransform.position;
    }

    public void hitForceCallback(Vector3 forceVector)
    {
        BastonImpulseController check = GetComponent<BastonImpulseController>();
        if(check != null)
        {
            
         _myEnemyStrikingForceController.StrikeCallback(forceVector);
        }
    }

    // private void OnCollisionEnter(Collision collision)
    // {
    //     BastonImpulseController check = GetComponent<BastonImpulseController>();
    //     if(check != null)
    //     {
           
    //     }
    
    //     EnemyLifeComponent otherCheck = GetComponent<EnemyLifeComponent>();
    
        
    // }
    
    public void ReleasePowerUp()
    {
        Instantiate(_myPowerUp, _myEnemy.transform);
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _thisTransform = transform;
        _myPowerUp = gameObject;
        _myEnemyStrikingForceController = gameObject.GetComponent<EnemyStrikingForceController>();
    }

        // Update is called once per frame
        void Update()
        {

        }
}
