using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_HealthManager : MonoBehaviour
{
    #region references
    CharacterMovementController _myMovementController;
    #endregion

    #region parameters
    ///<summary>
    ///Vida maxima que puede tener el jugador
    ///</summary>
    [SerializeField] private int _maxHealth = 8;
    ///<summary>
    ///Valor que determina la vida máxima
    ///</summary>
    [SerializeField] private int _currentHealth = 8;
    [SerializeField] private float _invulnerabilityTime = 1.0f;

    private bool isInvincible = false;
    #endregion

    #region methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Si se produce colisión entre enemigos baja la vida
        EnemyLifeComponent enemy = collision.gameObject.GetComponent<EnemyLifeComponent>();
        if (enemy != null && !isInvincible & !enemy.isDead)
        {
            ChangeHealthValue(-1, enemy.transform.position);
            StartCoroutine(InvulnerabilityTrigger(_invulnerabilityTime)); //Hace invulnerable a Chicho para que no le quite 20millones en un momento
        }
    }


    private IEnumerator InvulnerabilityTrigger(float invulnerabilityTime){
        isInvincible = true;
        yield return new WaitForSeconds(invulnerabilityTime);
        isInvincible = false;
    }
    ///<summary>
    ///Función que cambia el valor de vida del personaje. Importante poner el -
    ///en el valor que sea para meter daño, que normalmente usamos Damage(intdamage) y ya está
    ///</summary>
    public void ChangeHealthValue(int value, Vector3 damagerPosition)
    {
        if (!isInvincible)
        {
            _currentHealth += value;
            if (value < 0) _myMovementController.DamageImpulseRequest(damagerPosition);

            if (_currentHealth <= 0)
            {
                Die();
            }
            if (_currentHealth > _maxHealth)
            {
                //Hay que mantener el tope de vida
                _currentHealth = _maxHealth;
            }
            GameManager.Instance.OnHealthValueChange(_currentHealth);
            StartCoroutine(InvulnerabilityTrigger(_invulnerabilityTime)); //Hace invulnerable a Chicho para que no le quite 20millones en un momento
        }
    }

    public void ChangeHealthValue(int value)
    {
        if (!isInvincible)
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
            GameManager.Instance.OnHealthValueChange(_currentHealth);
            StartCoroutine(InvulnerabilityTrigger(_invulnerabilityTime)); //Hace invulnerable a Chicho para que no le quite 20millones en un momento
        }
    }


    public void Die()
    {
        Destroy(this.gameObject);
        GameManager.Instance.OnPlayerDeath();
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
        GameManager.Instance.OnHealthValueChange(_currentHealth);
        _myMovementController = GetComponent<CharacterMovementController>();
    }

    // Update is called once per frame
    void Update()
    {

        //Descomentar para usarlo de debug
        /*
        if(Input.GetKeyDown(KeyCode.Keypad7))
        {
            ChangeHealthValue(1);
        }
        if(Input.GetKeyDown(KeyCode.Keypad8))
        {
            ChangeHealthValue(-1);
        } 
        */               
    }
}
