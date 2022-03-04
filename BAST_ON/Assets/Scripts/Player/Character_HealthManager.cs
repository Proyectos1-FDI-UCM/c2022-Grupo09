using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_HealthManager : MonoBehaviour
{

    
    

  
    ///<summary>
    ///Vida maxima que puede tener el jugador
    ///</summary>
    [SerializeField] private int _maxHealth;

    ///<summary>
    ///Valor que determina la vida máxima
    ///</summary>
    [SerializeField] private int _currentHealth;

    
    

    
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
        if(_currentHealth > _maxHealth)
        {
           //Hay que mantener el tope de vida
           _currentHealth = _maxHealth;
       }
       GameManager.Instance.OnHealthValueChange(_currentHealth);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Si se produce colisión entre enemigos baja la vida
        EnemyLifeComponent enemy = collision.gameObject.GetComponent<EnemyLifeComponent>();
        if (enemy != null)
        {
            ChangeHealthValue(-1);
        }
    }

    public void Die()
    {
        Destroy(this.gameObject);
        GameManager.Instance.OnPlayerDeath();
    }


    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
        GameManager.Instance.OnHealthValueChange(_currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
