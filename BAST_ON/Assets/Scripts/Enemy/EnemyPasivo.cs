using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPasivo : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float speed = 3f, detectdist = 1f;
    /// <summary>
    /// Determina si un enemigo se quedar� quieto en un punto.
    /// En caso de estar en otro sitio, se mover� por defecto al l�mite derecho de la patrulla.
    /// </summary>
    private bool rotated = false, wallInfo;
    int layerF;
    #endregion

    #region references
    [SerializeField]
    private GameObject _detectorD, DetectorI;
    private SpriteRenderer _mySpriteRenderer;
    private Rigidbody2D _myRigidbody;
    private EnemyLifeComponent _myLifeComponent;
    //[SerializeField] private Transform _detector;
    #endregion

    #region properties
    private float _originalSpeed;
    private Vector2 _movementDirection, _detectorOrigin;
    private Transform _myDetectorD, _myDetectorI;
    private RaycastHit2D _wallInfoD, _wallInfoI;
    private RaycastHit2D _floorInfo;

    private Vector2 _gravity;
    private float _onAirElapsedTime = 0;
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
        layerF = LayerMask.GetMask("Floor");
        _mySpriteRenderer = GetComponent<SpriteRenderer>();
        _myRigidbody = GetComponent<Rigidbody2D>();
        _myLifeComponent = GetComponent<EnemyLifeComponent>();

        _myDetectorD = _detectorD.transform;

        _movementDirection = Vector2.right;
        _originalSpeed = speed;
    }

    private void Update()
    {
        // Ajuste de la rotaci�n del sprite del enemigo en funci�n de la direcci�n
        // Si el movimiento es hacia la izquierda lo gira
        _mySpriteRenderer.flipX = _movementDirection.x < 0;
        if (!rotated)
        {
            _detectorOrigin = _myDetectorD.position;
            _wallInfoD = Physics2D.Raycast(_detectorOrigin, Vector2.right, detectdist);
        }
        else
        {
            _detectorOrigin = _myDetectorI.position;
            _wallInfoI = Physics2D.Raycast(_detectorOrigin, Vector2.left, detectdist);
        }


        _floorInfo = Physics2D.Raycast(_detectorOrigin, Vector2.down, detectdist);

        if (!rotated) wallInfo = _wallInfoD.collider;
        else wallInfo = _wallInfoI.collider;

        if (!wallInfo || _floorInfo.collider)
        {
            _movementDirection = -_movementDirection;
            rotated = !rotated;
        }
    }

    private void FixedUpdate()
    {
        if (!_floorInfo) _onAirElapsedTime += Time.deltaTime;
        else _onAirElapsedTime = 0;

        

        // C�lculo de la gravedad
        _gravity = (Vector2.down * _myRigidbody.gravityScale * _onAirElapsedTime);

        // Llamada el m�todo del Life Component que recibe la direcci�n del movimiento
        _myLifeComponent.SetMovementDirection(_movementDirection);

        // Aplicaci�n del movimiento
        _myRigidbody.MovePosition(_myRigidbody.position + _gravity + _movementDirection.normalized * speed * Time.fixedDeltaTime);

        
    }

}
