using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrulla : MonoBehaviour
{
    [SerializeField]
    public float speed = 5f;
    [SerializeField]
    private bool rightMov = true;
    [SerializeField]
    private GameObject _limIzq, _limDer;
    private Vector3 _rightTarget, _leftTarget;

    private Vector3 _targetPosition;
    private Transform _myTransform;
    private Rigidbody2D _myRigidbody;

    private Vector2 _movementDirection;


    private void Start()
    {
        _myTransform = transform;
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
        _myTransform.rotation = Quaternion.identity;
        if (_movementDirection.x < 0) _myTransform.Rotate(Vector2.up, 180);
    }

    private void FixedUpdate()
    {
        _movementDirection = (Vector2)_targetPosition - _myRigidbody.position;

        _myRigidbody.MovePosition(_myRigidbody.position + _movementDirection.normalized * speed * Time.fixedDeltaTime);

        if (_targetPosition == (_rightTarget) && _myRigidbody.position.x >= _rightTarget.x) _targetPosition = _leftTarget;
        else if (_targetPosition == (_leftTarget) && _myRigidbody.position.x <= _leftTarget.x) _targetPosition = _rightTarget;
    }

}
