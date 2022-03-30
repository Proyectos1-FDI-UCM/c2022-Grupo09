using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ClimberPatrol : MonoBehaviour
{

    [SerializeField] private float speed = 1.0f;

    private Vector2 direction;
    private Transform _myTransform;
    private Rigidbody2D _myRigidbody;
    private bool isOnFloor = true;
    private bool isWallAhead = false;
    
    
    
    
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();
        _myTransform = GetComponent<Transform>();
        direction = _myTransform.right;
    }

    

    // Update is called once per frame
    void Update()
    {
        if(isOnFloor && !isWallAhead)
        {
            Move();
        }
        else
        {
            if(isWallAhead)
            {
                SetNewDirection(_myTransform.rotation.z + 90);
            }
            else
            {
                SetNewDirection(_myTransform.rotation.z - 90);
            }
        }
    }


    private void FixedUpdate() {
        
        RaycastHit2D wallHit = Physics2D.Raycast(_myTransform.position, _myTransform.forward, 1.0f);
        Debug.DrawRay(_myTransform.position, _myTransform.forward * 1.0f , Color.red);
        if (wallHit.collider != null)
        {
            Debug.Log("hit de raycast");
            isWallAhead = true;
        }
        else
        {
            isWallAhead = false;
        }

    }


    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("PatrolStop")){
            Flip();
        }
    }

    


    

    void Move()
    {
        _myRigidbody.MovePosition(_myRigidbody.position + (direction * speed * Time.deltaTime));
    }

    void SetNewDirection(float angle)
    {
        _myTransform.Rotate(new Vector3(0, 0, angle));
        
    }

    void Flip()
    {
        direction *= -1;
    }

    public void FloorUpdateReceiver(bool result)
    {
        Debug.Log("Se cambia el bool de isOnFloor: " + isOnFloor);
        isOnFloor = result;
    }


}
