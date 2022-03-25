using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ClimberPatrol : MonoBehaviour
{
    private Vector2 direction;

    private bool isOnFloor;
    private bool isWallAhead;
    
    
    
    
    void Start()
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {
        if(isOnFloor)
        {
            Move();
        }
        else
        {
            if(isWallAhead)
            {
                SetNewDirection();
            }
            else
            {
                SetNewDirection();
            }
        }
    }


    private void FixedUpdate() {
        
        RaycastHit2D wallHit = Physics2D.Raycast(transform.position, transform.forward, 0.01f, 8);

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
        
        
        if(/*colisiona con tope de ruta*/){
            Flip();
        }


        if(/*colisiona con suelo*/){
            isOnFloor = true;
        }
        else
        {
            isOnFloor = false;
        }




    }

    


    

    void Move()
    {
        
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
