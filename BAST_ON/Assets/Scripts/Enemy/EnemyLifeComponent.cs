using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeComponent : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private int _maxHealth = 3;
    [SerializeField]
    private int _dropPercentage = 50;
    #endregion

    #region properties
    [SerializeField]
    private int _currentHealth = 3;
    ///<summary>
    ///Valor que detecta si ha sido golpeado
    ///</summary>
    private bool _hasBeenHit = false;
    
    #endregion

    #region references 
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
        Destroy(gameObject);
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
        if(Random.Range(0, (100 / _dropPercentage)) == 0) Instantiate(_myPowerUp, _thisTransform.position, Quaternion.identity);
        
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _thisTransform = transform;
        _myEnemyStrikingForceController = gameObject.GetComponent<EnemyStrikingForceController>();
    }
}
