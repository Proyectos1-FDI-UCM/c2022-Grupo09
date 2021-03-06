using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrulla : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float speed = 5f;
    /// <summary>
    /// Determina si un enemigo se quedar� quieto en un punto.
    /// En caso de estar en otro sitio, se mover� por defecto al l�mite derecho de la patrulla.
    /// </summary>
    [SerializeField] private bool _staticEnemy = false;
    #endregion

    #region references
    [SerializeField]
    private GameObject _limIzq, _limDer;
    private Vector2 _rightTarget, _leftTarget;
    private SpriteRenderer _mySpriteRenderer;
    private Rigidbody2D _myRigidbody;
    private EnemyLifeComponent _myLifeComponent;
    private Animator _myAnimator;
    #endregion

    #region properties
    private float _originalSpeed;
    private Vector2 _targetPosition;
    private Vector2 _movementDirection;
    #endregion

    #region methods
    public void SlowDown(float speedReducer)
    {
        speed = _originalSpeed/speedReducer;
        _myAnimator.SetFloat("KiwiReducer", 1 / speedReducer);
    }
    private void SpeedUp()
    {
        speed = _originalSpeed;
        _myAnimator.SetFloat("KiwiReducer", 1);
    }
    #endregion


    private void Start()
    {
        _mySpriteRenderer = GetComponent<SpriteRenderer>();
        _myRigidbody = GetComponent<Rigidbody2D>();
        _myLifeComponent = GetComponent<EnemyLifeComponent>();

        _rightTarget = _limDer.transform.position;
        _leftTarget = _limIzq.transform.position;

        _targetPosition = _rightTarget;

        _originalSpeed = speed;
        _myAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        // Ajuste de la rotaci�n del sprite del enemigo en funci�n de la direcci�n
        // Si el movimiento es hacia la izquierda lo gira
        _mySpriteRenderer.flipX = _movementDirection.x < 0;

        // Comprobación de si hay un Kiwi Activo
        if (GameManager.Instance.GetKiwiActive()) SlowDown(GameManager.Instance.GetKiwiSlowDown());
        else SpeedUp();
    }

    private void FixedUpdate()
    {
        // C�lculo del movimiento
        _movementDirection = _targetPosition - _myRigidbody.position;

        // Si el enemigo es est�tico y est� suficientemente cerca del target no se mueve
        // Hecho para evitar que contin�e movi�ndose alante y atr�s cuando a efectos pr�cticos ya ha llegado al target
        if (_staticEnemy && (_movementDirection).magnitude < 0.1f) _movementDirection = Vector2.zero;

        // Llamada el m�todo del Life Component que recibe la direcci�n del movimiento
        _myLifeComponent.SetMovementDirection(_movementDirection);

        // Aplicaci�n del movimiento
        _myRigidbody.MovePosition(_myRigidbody.position + _movementDirection.normalized * speed * Time.fixedDeltaTime);

        // Cambio de target cuando se llega a uno de ellos
        if (!_staticEnemy)
        {
            if (_targetPosition == _rightTarget && _myRigidbody.position.x >= _targetPosition.x) _targetPosition = _leftTarget;
            else if (_myRigidbody.position.x <= _targetPosition.x) _targetPosition = _rightTarget;
        }
    }

}
