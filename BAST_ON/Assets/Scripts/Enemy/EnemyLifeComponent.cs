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

    [SerializeField]
    private int _currentHealth = 3;
    #region properties
    ///<summary>
    ///Valor que detecta si ha sido golpeado
    ///</summary>
    public bool isDead = false;
    private float _originalspeed;
    
    #endregion

    #region references 
    private Transform _thisTransform;
    

    [SerializeField]
    private GameObject _myPowerUp;
    private Animator _myAnimator;
    private AudioSource _myAudioSource;
    private EnemyPatrulla _myEnemyPatrulla;
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
         isDead = true;
         _myEnemyPatrulla.enabled = false;
         Destroy(gameObject, 0.45f);
        _myAudioSource.Play();
         _myAnimator.Play("Explosion");
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
        if(Random.Range(0, 100) < _dropPercentage) Instantiate(_myPowerUp, _thisTransform.position, Quaternion.identity);
        
    }

    public void SlowDown(EnemyPatrulla enemy)
    {
        _originalspeed = enemy.speed / 2;
    }

    public void DontSlowDown(EnemyPatrulla enemy)
    {
        _originalspeed = enemy.speed;
    }
   
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.SendEnemyLifeComponent(this);
        _myAudioSource = GetComponent<AudioSource>();
        _thisTransform = transform;
        _myEnemyStrikingForceController = gameObject.GetComponent<EnemyStrikingForceController>();
        _myAnimator = gameObject.GetComponent<Animator>();
        _myEnemyPatrulla = GetComponent<EnemyPatrulla>();
        _originalspeed = _myEnemyPatrulla.speed;
    }
}
