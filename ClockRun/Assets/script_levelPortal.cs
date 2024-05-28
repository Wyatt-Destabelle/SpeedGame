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

    public CameraControls cam;

    public GameObject screen;

    public GameObject player;

    public AudioSource winSFX;

    bool moving;
    // Start is called before the first frame update
    void Start()
    {
        moving = false;
        cam = Camera.main.GetComponent<CameraControls>();
    }

    // Update is called once per frame
    void Update()
    {
        if(moving)
        {
            if(screen.transform.position.y > 0)
            {
            player.transform.position = newLocation;
                cam.save = new Vector3(CameraX, 0, -10);
                cam.moon.transform.position = new Vector3(CameraX, 2, 0);
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
            winSFX.Play();
        }
    }
}
