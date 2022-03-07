using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_HealthManager : MonoBehaviour
{
    #region parameters    
    ///<summary>
    ///Vida maxima que puede tener el jugador
    ///</summary>
    [SerializeField] private int _maxHealth;
    ///<summary>
    ///Valor que determina la vida máxima
    ///</summary>
    [SerializeField] private int _currentHealth;
    #endregion

    #region methods
    private void OnCollisionEnter(Collision collision)
    {
        //Si se produce colisión entre enemigos baja la vida
        EnemyLifeComponent enemy = collision.gameObject.GetComponent<EnemyLifeComponent>();
        if (enemy != null)
        {
            ChangeHealthValue(-1);
        }
    }
    ///<summary>
    ///Función que cambia el valor de vida del personaje. Importante poner el -
    ///en el valor que sea para meter daño, que normalmente usamos Damage(intdamage) y ya está
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
