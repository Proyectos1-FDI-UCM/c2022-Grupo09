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
    [SerializeField]
    private int _damageToPlayer = 1;
    #endregion

    #region properties
    [SerializeField]
    private int _currentHealth = 3;
    ///<summary>
    ///Valor que detecta si ha sido golpeado
    ///</summary>
    public bool isDead = false;
    /// <summary>
    /// Valor que guarda la dirección del movimiento del enemigo para empujar al jugador en esa dirección.
    /// </summary>
    private Vector2 _movementDirection;
    #endregion

    #region references 
    private Transform _thisTransform;
    private Collider2D _myCollider;
    private DisparadorController _myDisparos;
    [SerializeField]
    private GameObject _myPowerUp;
    private Animator _myAnimator;
    private AudioSource _myAudioSource;
    private EnemyPatrulla _myEnemyPatrulla;
    private EnemyStrikingForceController _myEnemyStrikingForceController;
    #endregion

    #region methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Character_HealthManager player = collision.gameObject.GetComponent<Character_HealthManager>();
        if (player != null)
        {
            player.ChangeHealthValue(-_damageToPlayer, _movementDirection);
        }
    }
    /// <summary>
    /// Recibe la dirección de movimiento del enemigo para impulsar al jugador en esa dirección.
    /// </summary>
    /// <param name="movementDirection"></param>
    public void SetMovementDirection(Vector2 movementDirection)
    {
        _movementDirection = movementDirection;
    }

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
        Destroy(gameObject, 0.45f);
        GameManager.Instance.OnEnemyDies(this);
        _myAnimator.Play("Explosion");
        _myAudioSource.Play();
        _myCollider.enabled = false;
        if (_myDisparos != null) _myDisparos.enabled = false;
    }


    public void ReleasePowerUp()
    {
        if (Random.Range(0, 100) < _dropPercentage) Instantiate(_myPowerUp, _thisTransform.position, Quaternion.identity);

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
        _myCollider = GetComponent<Collider2D>();
        _myDisparos = GetComponent<DisparadorController>();
    }
}