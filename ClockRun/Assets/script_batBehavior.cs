using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class script_batBehavior : MonoBehaviour
{
    public float kickForce,hitForce;
    Transform batTransform;
    Rigidbody2D playerRB;
    bool swinging,kickAllowed;
    int swingTimer;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider2D>().enabled = false;
        playerRB = GetComponentInParent<Rigidbody2D>();
        batTransform = GetComponent<Transform>();
        swingTimer = 0;
        kickAllowed = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.right = new Vector3(mousePos.x,mousePos.y,transform.position.z) - transform.position;

        if(Input.GetKeyDown(KeyCode.F))
        {
            GetComponent<BoxCollider2D>().enabled = true;
            swinging = true;
            kickAllowed = true;
            swingTimer = 15;
        }
        
    }
    void FixedUpdate()
    {
        if(swingTimer > 0)
        {
            
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
            playerRB.velocity = Vector2.zero;
            playerRB.AddForce(transform.right * -kickForce,ForceMode2D.Impulse);
        }
        if(col.gameObject.tag.Equals("Gear"))
        {
            col.GetComponent<Rigidbody2D>().AddForce(transform.right*hitForce,ForceMode2D.Impulse);
        }

    }
}
