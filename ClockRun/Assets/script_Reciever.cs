using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class script_Reciever : MonoBehaviour
{
    public GameObject Screen;
    public GameObject gameManager;
    public Vector2 nextLevel;
    public float CameraX;
    public GameObject portal;
        public float DistancePerGear;
    public float gearsNeeded;
    float gearsTaken;
    float MoveTime;

    public AudioSource lockSFX;

    Vector3 origPos;

    // Start is called before the first frame update
    void Start()
    {
        origPos = transform.position;
        gearsTaken = 0;
        MoveTime = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(gearsNeeded == gearsTaken)
        {
            
            GameObject g = Instantiate(portal,transform);
            g.GetComponent<script_levelPortal>().newLocation = nextLevel;
            g.GetComponent<script_levelPortal>().CameraX = CameraX;
            g.GetComponent<script_levelPortal>().screen = Screen;
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
            MoveTime += 10;
        }
    }

    public void resetPos()
    {
        transform.position = origPos;
    }

}
