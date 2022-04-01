using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPasivo : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float speed = 5f, detectdist = 1f;
    /// <summary>
    /// Determina si un enemigo se quedar� quieto en un punto.
    /// En caso de estar en otro sitio, se mover� por defecto al l�mite derecho de la patrulla.
    /// </summary>
    [SerializeField] private bool _staticEnemy = false;
    private bool rotated = false;
    #endregion

    #region references
    [SerializeField]
    private GameObject _detector;
    private SpriteRenderer _mySpriteRenderer;
    private Rigidbody2D _myRigidbody;
    private EnemyLifeComponent _myLifeComponent;
    //[SerializeField] private Transform _detector;
    #endregion

    #region properties
    private float _originalSpeed;
    private Vector2 _movementDirection, _detectorOrigin;
    private Transform _myDetector;
    private RaycastHit2D _wallInfo;
    private RaycastHit2D _floorInfo;
    #endregion

    #region methods
    public void SlowDown(float speedReducer)
    {
        speed = _originalSpeed / speedReducer;
    }
    public void RestoreSpeed()
    {
        speed = _originalSpeed;
    }
    #endregion


    private void Start()
    {
        _mySpriteRenderer = GetComponent<SpriteRenderer>();
        _myRigidbody = GetComponent<Rigidbody2D>();
        _myLifeComponent = GetComponent<EnemyLifeComponent>();

        _myDetector = _detector.transform;

        _movementDirection = Vector2.right;
        _originalSpeed = speed;
    }

    /*void Update()
    {   
        
        if (rightMov)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.position.x >= _limDer.transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
            else if (transform.position.x <= _limIzq.transform.position.x)

            {

                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }else if (!rightMov)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            
            if (transform.position.x <= _limIzq.transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
            else if (transform.position.x >= _limDer.transform.position.x)
            {

                transform.eulerAngles = new Vector3(0, 0, 0);
            }


        }
    }*/
    private void Update()
    {
        // Ajuste de la rotaci�n del sprite del enemigo en funci�n de la direcci�n
        // Si el movimiento es hacia la izquierda lo gira
        if (!rotated) _detectorOrigin = _myDetector.position;
        else _detectorOrigin.x = _myDetector.position.x - 2;
        _mySpriteRenderer.flipX = _movementDirection.x < 0;
        _wallInfo = Physics2D.Raycast(_detectorOrigin, _movementDirection, detectdist);
        _floorInfo = Physics2D.Raycast(_detectorOrigin, Vector2.down, detectdist);
    }

    private void FixedUpdate()
    {
        // C�lculo del movimiento

        // Si el enemigo es est�tico y est� suficientemente cerca del target no se mueve
        // Hecho para evitar que contin�e movi�ndose alante y atr�s cuando a efectos pr�cticos ya ha llegado al target
        if (_staticEnemy && (_movementDirection).magnitude < 0.1f) _movementDirection = Vector2.zero;

        // Llamada el m�todo del Life Component que recibe la direcci�n del movimiento
        _myLifeComponent.SetMovementDirection(_movementDirection);

        // Aplicaci�n del movimiento
        _myRigidbody.MovePosition(_myRigidbody.position + _movementDirection.normalized * speed * Time.fixedDeltaTime);

        // Cambio de target cuando se llega a uno de ellos
        if (_wallInfo.collider||!_floorInfo.collider)
        {
            _movementDirection = -_movementDirection;
            rotated = !rotated;
        }
    }

}
