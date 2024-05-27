using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class script_levelPortal : MonoBehaviour
{
    public String nextLevel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag.Equals("Player"))
        {
            SceneManager.LoadScene(nextLevel);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextLevel));

        }
    }
}
