using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour, IDataPersistence
{
    [SerializeField] private float MoveSpeed = 10;
    private BoxCollider2D boxCollider;
    private Animator Animator;
    private RaycastHit2D castResult;
    [SerializeField] LayerMask blockLayerMask;
    

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        Animator = GetComponent<Animator>();

        //our character should persist between scenes
        DontDestroyOnLoad(gameObject);
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
        
        // Check if we are hitting something in X-Axis
        castResult = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveX, 0), Mathf.Abs(moveX * Time.fixedDeltaTime * MoveSpeed), blockLayerMask);
        
        if (castResult.collider)
        {
            //STOP moving x-axis
            moveDelta.x = 0;
        }

        // Check if we are hitting something in Y-AxisS
        castResult = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveY), Mathf.Abs(moveY * Time.fixedDeltaTime * MoveSpeed), blockLayerMask);

        if (castResult.collider)
        {
            //STOP moving Y-axis
            moveDelta.y = 0;
        }

       //makes animation happen
        bool isRunning = moveDelta.magnitude > 0;
        Animator.SetBool("isRunning", isRunning);
        transform.Translate(MoveSpeed * Time.fixedDeltaTime * moveDelta);

    }

    public void LoadData(GameData data)
    {
        this.transform.position = data.mainPlayerPosition;
    }

    public void SaveData(GameData data)
    {
        data.mainPlayerPosition = this.transform.position;
    }
}

