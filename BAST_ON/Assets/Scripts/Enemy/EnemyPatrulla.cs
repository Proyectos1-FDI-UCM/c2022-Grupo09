using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrulla : MonoBehaviour
{
    #region parameters
    [SerializeField]
    public float speed = 5f;
    #endregion

    #region references
    [SerializeField]
    private GameObject _limIzq, _limDer;
    private Vector2 _rightTarget, _leftTarget;
    private SpriteRenderer _mySpriteRenderer;
    private Rigidbody2D _myRigidbody;
    #endregion

    #region properties
    private Vector2 _targetPosition;
    private Vector2 _movementDirection;
    #endregion


    private void Start()
    {
        _mySpriteRenderer = GetComponent<SpriteRenderer>();
        _myRigidbody = GetComponent<Rigidbody2D>();

        _rightTarget = _limDer.transform.position;
        _leftTarget = _limIzq.transform.position;

        _targetPosition = _rightTarget;
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
        // Ajuste de la rotación del sprite del enemigo en función de la dirección
        // Si el movimiento es hacia la izquierda lo gira
        _mySpriteRenderer.flipX = _movementDirection.x < 0;
    }

    private void FixedUpdate()
    {
        // Cálculo  y aplicación del movimiento
        _movementDirection = _targetPosition - _myRigidbody.position;
        _myRigidbody.MovePosition(_myRigidbody.position + _movementDirection.normalized * speed * Time.fixedDeltaTime);

        // Cambio de target cuando se llega a uno de ellos
        if (_targetPosition == _rightTarget && _myRigidbody.position.x >= _targetPosition.x) _targetPosition = _leftTarget;
        else if (_myRigidbody.position.x <= _targetPosition.x) _targetPosition = _rightTarget;
    }

}
