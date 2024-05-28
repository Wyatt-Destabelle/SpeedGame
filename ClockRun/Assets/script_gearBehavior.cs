using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class script_gearBehavior : MonoBehaviour
{
    Vector3 origPos;
     Rigidbody2D gearRB;
    public float intertia;

    public bool collected = false;
    public GameObject target;
    float timer = 1f;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        origPos = transform.position;
        gearRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        anim.SetFloat("Speed", gearRB.velocity.magnitude * .5f);

        if(gearRB.velocity.magnitude > 0)
            gearRB.velocity =  Vector2.MoveTowards(gearRB.velocity,Vector2.zero, intertia);


        if (collected)
        {
            gearRB.transform.position = Vector3.Lerp(gearRB.transform.position, target.transform.position, .1f);
            timer -= .01f;
            gearRB.transform.localScale = Vector3.one * Mathf.Max(timer,0f);

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag.Equals("Ground")|| col.gameObject.tag == "Wall")
        {
            Vector3 point = col.ClosestPoint(transform.position);
            point = point - transform.position;
            if(Math.Abs(point.x) > Math.Abs(point.y))
            {
                gearRB.velocity = new Vector2(-gearRB.velocity.x,gearRB.velocity.y);
            }
            else
            {
                gearRB.velocity = new Vector2(gearRB.velocity.x,-gearRB.velocity.y);
            }
        }
    }

    public void resetPos()
    {
        transform.position = origPos;
    }

    

}
