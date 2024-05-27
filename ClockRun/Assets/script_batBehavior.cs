using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class script_batBehavior : MonoBehaviour
{
    float dashTimer;
    public float kickForce,hitForce;
    Transform batTransform;
    Rigidbody2D playerRB;
    bool swinging,kickAllowed;
    int swingTimer;

    Animator animationController;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider2D>().enabled = false;
        playerRB = GetComponentInParent<Rigidbody2D>();
        batTransform = GetComponent<Transform>();
        animationController = GetComponentInParent<Animator>();
        swingTimer = 0;
        kickAllowed = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.right = new Vector3(mousePos.x,mousePos.y,transform.position.z) - transform.position;

        if(Input.GetKeyDown(KeyCode.F) && swingTimer == 0)
        {
            
            swinging = true;
            Time.timeScale = .5f;


        }
        if(Input.GetKeyUp(KeyCode.F) && swingTimer == 0)
        {
                                    animationController.SetBool("Swinging",true);
            float angle = Vector2.SignedAngle(Vector2.right,transform.right);
            if(angle < 22.5 && angle >= -22.5)
                animationController.SetInteger("SwingType",2);
            else if(angle < -22.5 && angle >= -67.5)
                animationController.SetInteger("SwingType",1);
            else if(angle < -67.5 && angle >= -102.5)
                animationController.SetInteger("SwingType",0);
            else if(angle < 67.5 && angle >= 22.5)
                animationController.SetInteger("SwingType",3);
            else if(angle < 102.5 && angle > 67.5)
                animationController.SetInteger("SwingType",4);
            Time.timeScale = 1;
            GetComponent<BoxCollider2D>().enabled = true;
            kickAllowed = true;
            swingTimer = 30;
        }

        
    }
    void FixedUpdate()
    {
        if(dashTimer > 0)
        {
            dashTimer -= 1;
            if(dashTimer == 0)
            {
                            GetComponentInParent<script_playerMovement>().movementLockout = false;
            }

        }
        if(swingTimer > 0)
        {
            animationController.SetBool("Swinging", false);
            swingTimer -= 1;
            if(swingTimer == 0)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                swinging = false;
                
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if((col.gameObject.tag.Equals("Ground")||col.gameObject.tag.Equals("Wall"))  && kickAllowed)
        {
            kickAllowed = false;
            if(transform.right.y < 0)
            GetComponentInParent<script_playerMovement>().grounded = false;
            GetComponentInParent<script_playerMovement>().movementLockout = true;
            dashTimer = 5;
            playerRB.velocity = Vector2.zero;
            playerRB.AddForce(transform.right * -kickForce,ForceMode2D.Impulse);
        }
        if(col.gameObject.tag.Equals("Gear"))
        {
            col.GetComponent<Rigidbody2D>().AddForce(transform.right*hitForce,ForceMode2D.Impulse);
        }

    }
}
