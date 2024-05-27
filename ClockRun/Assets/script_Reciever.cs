using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class script_Reciever : MonoBehaviour
{
    public String nextLevel;
    public GameObject portal;
        public float DistancePerGear;
    public float gearsNeeded;
    float gearsTaken;
    float MoveTime;

    public AudioSource lockSFX;

    // Start is called before the first frame update
    void Start()
    {
        gearsTaken = 0;
        MoveTime = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(gearsNeeded == gearsTaken)
        {
            GameObject g = Instantiate(portal,transform);
            g.GetComponent<script_levelPortal>().nextLevel = nextLevel;
            gearsNeeded = -1;
        }

        if(MoveTime > 0)
        {
            transform.position = transform.position + DistancePerGear*Vector3.up*Time.fixedDeltaTime;
            MoveTime -= 1;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Gear")
        {
            col.GetComponent<script_gearBehavior>().collected = true;
            col.GetComponent<script_gearBehavior>().target = gameObject;
            lockSFX.Play();
            Destroy(col);
            gearsTaken += 1;
            MoveTime = 10;
        }
    }
}
