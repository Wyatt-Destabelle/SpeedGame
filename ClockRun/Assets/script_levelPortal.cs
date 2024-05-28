using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class script_levelPortal : MonoBehaviour
{
    public Vector2 newLocation;
    public float CameraX;

    public GameObject screen;

    public GameObject player;

    bool moving;
    // Start is called before the first frame update
    void Start()
    {
        moving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(moving)
        {
            if(screen.transform.position.y > 0)
            {
            player.transform.position = newLocation;
            Camera.main.transform.position = new Vector3(CameraX,0,-10);
            screen.transform.position = new Vector3(CameraX,screen.transform.position.y,screen.transform.position.z);
            moving = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        
        if(col.gameObject.tag.Equals("Player"))
        {
            screen.GetComponent<script_Screen>().moving = true;
            moving = true;
            GetComponent<Collider2D>().enabled = false;
            player = col.gameObject;
        }
    }
}
