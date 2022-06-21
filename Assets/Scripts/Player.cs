using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed = 5;
    private BoxCollider2D BoxCollider;
    private Animator Animator;


    private void Start()
    {
        BoxCollider = GetComponent<BoxCollider2D>();
        Animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Movement();
 
    }

    private void Movement()
    {
        //Get the input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        //Store it in vector
        Vector2 moveDelta = new Vector2(moveX, moveY);

        //Flip player according to move direction
        if (moveDelta.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);

        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        //Collision check
        RaycastHit2D castResult;
        // Check if we are hitting something in X-Axis
        castResult = Physics2D.BoxCast(transform.position, BoxCollider.size, 0, new Vector2(moveX, 0), Mathf.Abs(moveX * Time.fixedDeltaTime * MoveSpeed), LayerMask.GetMask("BlockMove"));
        
        if (castResult.collider)
        {
            //STOP moving x-axis
            moveDelta.x = 0;
        }

        // Check if we are hitting something in Y-AxisS
        castResult = Physics2D.BoxCast(transform.position, BoxCollider.size, 0, new Vector2(0, moveY), Mathf.Abs(moveY * Time.fixedDeltaTime * MoveSpeed), LayerMask.GetMask("BlockMove"));

        if (castResult.collider)
        {
            //STOP moving Y-axis
            moveDelta.y = 0;
        }

       //makes animation happen
        bool isRunning = moveDelta.magnitude > 0;
        Animator.SetBool("isRunning", isRunning);
        transform.Translate(moveDelta * Time.fixedDeltaTime * MoveSpeed);

    }
    
}

