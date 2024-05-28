using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class script_GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    float currentTime;
    public GameObject G;
    TextMeshProUGUI T;

    public Vector3 respawnPoint;

    public bool death;
    public bool win;
    public GameObject player;
    public List<GameObject> list;
    void Start()
    {
        death = false;
    currentTime = 0;
    T = G.GetComponent<TextMeshProUGUI>();
    win = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!win)
        {
        currentTime += Time.deltaTime;
        String text = currentTime.ToString("#.0");
        T.text = text;
        }
        else
        {
        String text = currentTime.ToString("#.0");
            T.text = "Congratulations! Your time was: "+ text +" ! Press f to restart.";
        }
        if(Input.GetKeyDown(KeyCode.F) && win)
            SceneManager.LoadScene("LevelZero");
        if(death)
        {
                    SceneManager.LoadScene("LevelZero");

        }
    


    }
}
