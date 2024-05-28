using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class script_playerMovement : MonoBehaviour
{
    public bool movementLockout;
    Animator aController;
     Rigidbody2D playerRB;
    Transform playerTransform;

    public float groundAcceleration, airAcceleration, groundFriction, airFriction,topSpeed;

    public float risingGravity, fallingGravity, jumpForce;

    public bool grounded;

    float bufferJump;

    float last;

    public AudioSource
        jumpSFX,
        landSFX,
        walkSFX;

    public bool lookDir;
    SpriteRenderer spriteController;
    
    // Start is called before the first frame update
    void Start()
    {
        movementLockout = false;
        aController = GetComponent<Animator>();
        grounded =  false;
        playerRB = GetComponent<Rigidbody2D>();
        playerTransform = GetComponent<Transform>();
        spriteController = GetComponent<SpriteRenderer>();
        last = 0;
    }

    // Update is called once per frame
    void Update()
    {
        spriteController.flipX = lookDir;

        bufferJump -= Time.deltaTime;
        if (grounded && bufferJump >= 0f)
        {
            jumpSFX.Play();
            playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            bufferJump = -10f;
        }

        float xIn = Input.GetAxis("Horizontal");
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded)
            {
                jumpSFX.Play();
                playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            else
            {
                bufferJump = .2f;
            }
            
            //grounded = false;
        }
        if(Input.GetKey(KeyCode.Space) && !grounded)
        {
            risingGravity = .45f;
        }
        if(Input.GetKeyUp(KeyCode.Space))
            risingGravity = .55f;
    }
    void FixedUpdate()
    {
        if(!grounded)
        {
            ApplyGravity();
        }
        Move();
    }
    void ApplyGravity()
    {
        if(playerRB.velocity.y > 0)
            playerRB.velocity = playerRB.velocity + Vector2.down*risingGravity;
        else
            playerRB.velocity = playerRB.velocity + Vector2.down*fallingGravity;
    }
    void Move()
    {
        if(movementLockout)
            return;
        float A,F;
        if(grounded)
        {
            A = groundAcceleration;
            F = groundFriction;
        }
        else
        {
            A = airAcceleration;
            F = airFriction;
        }

        float xIn = Input.GetAxis("Horizontal");
        if(Math.Abs(xIn) > .15)
        {
            if(Math.Abs(playerRB.velocity.x) < topSpeed || Math.Sign(playerRB.velocity.x) != Math.Sign(xIn))
            {
              playerRB.velocity = Vector2.MoveTowards(playerRB.velocity,Vector2.right*topSpeed*xIn + Vector2.up*playerRB.velocity.y,A);
     
            }
            if(Mathf.Abs(xIn) < Mathf.Abs(last))
            {
                playerRB.velocity = Vector2.MoveTowards(playerRB.velocity,Vector2.right*topSpeed*xIn + Vector2.up*playerRB.velocity.y,A);
                aController.SetBool("Moving",false);
            }
            else
            {
                aController.SetBool("Moving",true);
            }
        }
        
        else
        {
            playerRB.velocity = Vector2.MoveTowards(playerRB.velocity,Vector2.up*playerRB.velocity.y,F);
            aController.SetBool("Moving",false);
        }
        last = xIn;
    }
    /*
    void OnCollisionEnter2D(Collision2D col)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.right *.25f - transform.up * .15f, -transform.up,.5f,(1<<3));
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position-Vector3.right *.25f- transform.up * .15f, -transform.up,.5f,(1<<3));
        if(col.gameObject.tag == "Ground" && (hit.collider != null ||  hit2.collider != null))
        {
        playerRB.velocity.Set(playerRB.velocity.x,0);
        grounded = true;
        aController.SetBool("Grounded",true);
        }
        
    }

    void OnCollisionExit2D(Collision2D col)
    {

        if(col.gameObject.tag == "Ground")
        {
        playerRB.velocity.Set(playerRB.velocity.x,0);
        grounded = false;
        aController.SetBool("Grounded",false);
        }
    }
    */
}
