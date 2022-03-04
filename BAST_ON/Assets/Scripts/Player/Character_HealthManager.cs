using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_HealthManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _myPlayer;

  
    ///<summary>
    ///Vida maxima que puede tener el jugador
    ///</summary>
    [SerializeField] private int _maxHealth;

    ///<summary>
    ///Valor que determina la vida máxima
    ///</summary>
    [SerializeField] private int _currentHealth;

    [SerializeField] private int _hitDamage;

    ///<summary>
    ///Array que determina la vida en el UI
    ///</summary>

    
    [SerializeField] private Image[] _fruitArray;
    
    ///<summary>
    ///Referencia al sprite de la vida llena
    ///</summary>
    [SerializeField] private Sprite fullFruit;
    ///<summary>
    ///Referencia al sprite de la vida vacía
    ///</summary>
    [SerializeField] private Sprite emptyFruit;

    private void updateLifeBar(){
        for(int i = 0; i < _currentHealth; i++)
        {
            _fruitArray[i].sprite = fullFruit;
        }
        for(int i = _currentHealth; i < _maxHealth; i++)
        {
            _fruitArray[i].sprite = emptyFruit;
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
        if(_currentHealth > _maxHealth)
        {
           //Hay que mantener el tope de vida
           _currentHealth = _maxHealth;
        }
       
       updateLifeBar();
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Si se produce colisión entre enemigos baja la vida
        EnemyLifeComponent enemy = collision.gameObject.GetComponent<EnemyLifeComponent>();
        if (enemy != null)
        {
            Damage();
        }
    }

    public void Damage()
    {
        _currentHealth -= _hitDamage;
    }

    public void Die()
    {
        Destroy(_myPlayer);
    }


    // Start is called before the first frame update
    void Start()
    {
        _maxHealth = _fruitArray.Length;
        _currentHealth = _maxHealth;
        updateLifeBar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
