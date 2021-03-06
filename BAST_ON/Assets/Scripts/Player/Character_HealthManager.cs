using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_HealthManager : MonoBehaviour
{
    #region references
    CharacterMovementController _myMovementController;
    SpriteRenderer _mySpriteRenderer;
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
    private float _currentTime=1, _blinkTime=0.2f;
    [SerializeField] private float _invulnerabilityTime = 1.0f;

    private bool blink = false, isInvincible = false;
    #endregion

    #region methods
    private IEnumerator InvulnerabilityTrigger(float invulnerabilityTime){
        blink = true;
        Physics2D.IgnoreLayerCollision(6, 7, true);
         yield return new WaitForSeconds(invulnerabilityTime);
        Physics2D.IgnoreLayerCollision(6, 7, false);
        blink = false;
    }

    /// <summary>
    /// Método que cambia el valor de la vida del personaje.
    /// Recibe el valor de cambio de vida y la dirección en la que se quiere mover al jugador al recibir daño.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="damagerPosition"></param>
    public void ChangeHealthValue(int value, Vector3 damageImpulse)
    {
        if (value < 0) _myMovementController.DamageImpulseRequest(damageImpulse);
        ChangeHealthValue(value);        
    }
    ///<summary>
    ///Función que cambia el valor de vida del personaje. Importante poner el -
    ///en el valor que sea para meter daño, que normalmente usamos Damage(int damage) y ya está
    ///</summary>
    public void ChangeHealthValue(int value)
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
        if(value < 0) StartCoroutine(InvulnerabilityTrigger(_invulnerabilityTime)); //Hace invulnerable a Chicho para que no le quite 20millones en un momento
    }

    public void Die()
    {
        gameObject.SetActive(false);
        // Desactivar invulnerabilidad al morir para que no esté activo al respawn
        Physics2D.IgnoreLayerCollision(6, 7, false);
        blink = false;
        GameManager.Instance.OnPlayerDeath();
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
        GameManager.Instance.OnHealthValueChange(_currentHealth);
        _myMovementController = GetComponent<CharacterMovementController>();
        _mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime += Time.deltaTime;
        if (blink&&_currentTime<=_blinkTime)
        {
            _mySpriteRenderer.enabled = false;
            
        }
        else if(blink&&_currentTime> _blinkTime&&_currentTime<_blinkTime*2)
        {
            _mySpriteRenderer.enabled = true;
        }
        else if(!blink)
        {
            _mySpriteRenderer.enabled = true;
        }
        else
        {
            _currentTime = 0;
        }
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
