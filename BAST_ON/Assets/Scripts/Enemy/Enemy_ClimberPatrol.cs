using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ClimberPatrol : MonoBehaviour
{

    [SerializeField] private float speed = 1.0f;

    private Vector2 direction;
    private Transform _myTransform;
    private Rigidbody _myRigidbody;
    private bool isOnFloor;
    private bool isWallAhead;
    
    
    
    
    void Start()
    {
        _myTransform = gameObject.GetComponent<Transform>();
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
                SetNewDirection(_myTransform.up);
            }
            else
            {
                SetNewDirection(-_myTransform.up);
            }
        }
    }


    private void FixedUpdate() {
        
        RaycastHit2D wallHit = Physics2D.Raycast(_myTransform.position, _myTransform.forward, 0.51f, 8);

        if (wallHit.collider != null)
        {
            isWallAhead = true;
        }
        else
        {
            isWallAhead=false;
        }

    }


    private void OnCollisionEnter2D(Collision2D other) {
        
        

        if(other.gameObject.CompareTag("PatrolStop")){
            Flip();
        }

        
        if(/*colisiona con suelo*/false){
            isOnFloor = true;
        }
        else
        {
            isOnFloor = false;
        }




    }

    


    

    void Move()
    {
        _myRigidbody.MovePosition(direction * speed * Time.deltaTime);
    }

    void SetNewDirection(Vector2 newDirection)
    {
        direction = newDirection;
        
    }

    void Flip()
    {
        direction *= -1;
    }




}
