using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeComponent : MonoBehaviour
{
    #region references 
    [SerializeField]
    private int _currentHealth = 1;

    [SerializeField]
    private int _maxHealth = 1;

    [SerializeField]
    private GameObject _myEnemy;


    ///<summary>
    ///Valor que detecta si ha sido golpeado
    ///</summary>
    private bool _hasBeenHit = false;

    private Transform _thisTransform;

    #endregion

    #region methods

    public void ChangeHealth(int value)
    {
        _currentHealth += value;

        if (_currentHealth <= 0)
        {
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

   
    private void OnCollisionEnter(Collision collision)
    {
        BastonImpulseController check = GetComponent<BastonImpulseController>();
        if(check != null)
        {
            


            return;
        }


        EnemyLifeComponent otherCheck = GetComponent<EnemyLifeComponent>();
        
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _thisTransform = gameObject.GetComponent<Transform>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
