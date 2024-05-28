using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class script_hurt : MonoBehaviour
{
    public GameObject gm;
    public GameObject screen;
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
            if(screen.transform.position.y > 0)
                {
                            gm.GetComponent<script_GameManager>().death = true;
                }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            screen.GetComponent<script_Screen>().moving = true;
            moving = true;
            GetComponent<Collider2D>().enabled = false;
        }
            

    }
}
