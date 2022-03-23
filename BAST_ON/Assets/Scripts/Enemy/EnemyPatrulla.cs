using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrulla : MonoBehaviour
{
    [SerializeField]
    public float speed;
    [SerializeField]
    private bool rightMov = true;
    [SerializeField]
    private GameObject _limIzq, _limDer;
    private Vector3 _rightTarget, _leftTarget;

    private Vector3 _targetPosition;
    private Transform _myTransform;
    private Rigidbody2D _myRigidbody;

    private Vector3 _movementDirection;


    private void Start()
    {
        _myTransform = transform;
        _myRigidbody = GetComponent<Rigidbody2D>();

        _rightTarget = _limDer.transform.position;
        _leftTarget = _limIzq.transform.position;

        _targetPosition = _rightTarget;
    }

    void Update()
    {   
        
        /*if (rightMov)
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


        }*/

        _movementDirection = _targetPosition - _myTransform.position;

        _myRigidbody.MovePosition(_movementDirection * speed * Time.deltaTime);

        _myTransform.rotation = Quaternion.identity;
        if (_movementDirection.x < 0) _myTransform.Rotate(Vector2.up, 180);

        if (_myTransform.position == _targetPosition && _targetPosition == _rightTarget) _targetPosition = _leftTarget;
        else if (_myTransform.position == _targetPosition) _targetPosition = _rightTarget;
    }

}
