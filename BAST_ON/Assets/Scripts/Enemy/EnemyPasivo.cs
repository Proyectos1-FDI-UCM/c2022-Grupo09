using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPasivo : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float speed = 1, distance = 1;
    private bool rotate = true;
    private int layerF,layerE;
    #endregion

    #region references
    [SerializeField]
    private Transform _detector, _topDetector;
    private Rigidbody2D _myRigidBody;
    private RaycastHit2D _groundDetect, _wallDetect, _enemyDetect;
    private FloorDetector _myFloorDetector;
    private Animator _myAnimator;
    #endregion

    #region properties
    private Vector2 _movementDirection;
    private Vector2 _gravity;
    private float _onAirElapsedTime;
    private float _originalSpeed;
    #endregion

    #region methods
    private void SlowDown(float slowDown)
    {
        speed = _originalSpeed / slowDown;
        _myAnimator.SetFloat("KiwiReducer", 1 / slowDown);
    }
    private void SpeedUp()
    {
        speed = _originalSpeed;
        _myAnimator.SetFloat("KiwiReducer", 1);
    }
    #endregion


    private void Start()
    {
        layerF = LayerMask.GetMask("Floor");
        layerE = LayerMask.GetMask("Enemies");
        _myRigidBody = GetComponent<Rigidbody2D>();
        _movementDirection = Vector2.right;
        _myFloorDetector = GetComponent<FloorDetector>();
        _originalSpeed = speed;
        _myAnimator = GetComponent<Animator>();
    }


    private void Update()
    {
        // Centro del boxcast en medio de los detectores en la vertical y en la horizontal en la posicion del detector más la mitad de la longitud de la caja (multiplicado por la dirección de movimiento para restar o sumar dependiendo de esta)
        Vector2 center = new Vector2(_detector.position.x + (_movementDirection.normalized.x) * distance / 2, (_topDetector.position.y + _detector.position.y)/2);
        // Tamaño del boxcast: distancia escogida en la horizontal y la diferencia entre ambos detectores
        Vector2 size = new Vector2(distance, _topDetector.position.y - _detector.position.y);
        _groundDetect = Physics2D.Raycast(_detector.position, Vector2.down, distance,layerF);
        _wallDetect = Physics2D.BoxCast(center, size, 0, _movementDirection, 0, layerF);
        _enemyDetect = Physics2D.BoxCast(center, size, 0, _movementDirection, 0, layerE);
        //_wallDetect = Physics2D.Raycast(_detector.position, _movementDirection, distance, layerF);
        //_enemyDetect = Physics2D.Raycast(_detector.position, _movementDirection, distance, layerE);

        if (!_groundDetect.collider || _wallDetect.collider||_enemyDetect)
        {
            if (rotate)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                _movementDirection = -_movementDirection;
                rotate = !rotate;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                _movementDirection = -_movementDirection;
                rotate = !rotate;
            }
        }

        // Comprobación de si hay un Kiwi Activo
        if (GameManager.Instance.GetKiwiActive()) SlowDown(GameManager.Instance.GetKiwiSlowDown());
        else SpeedUp();

    }
    private void FixedUpdate()
    {   // Calculo de la gravedad
        _gravity = Vector2.down * _onAirElapsedTime;
        _myRigidBody.MovePosition(_myRigidBody.position + (_gravity + _movementDirection.normalized * speed) * Time.fixedDeltaTime);
        // Contador del tiempo en el aire
        if (!_myFloorDetector.IsGrounded()) _onAirElapsedTime += Time.fixedDeltaTime;
        else _onAirElapsedTime = 0f;
    }
}
