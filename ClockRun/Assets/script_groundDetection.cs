using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class script_groundDetection : MonoBehaviour
{
    Rigidbody2D playerRB;
    Animator aController;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponentInParent<Rigidbody2D>();
        aController = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag.Equals("Ground"))
        {
            GetComponentInParent<script_playerMovement>().grounded = true;
            playerRB.velocity.Set(playerRB.velocity.x,0);
            aController.SetBool("Grounded",true);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {

        if(col.gameObject.tag == "Ground")
        {
        playerRB.velocity.Set(playerRB.velocity.x,0);
        GetComponentInParent<script_playerMovement>().grounded = false;
        aController.SetBool("Grounded",false);
        }
    }
}
