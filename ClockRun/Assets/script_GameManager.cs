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

    public bool death;
    void Start()
    {
        death = false;
    currentTime = 0;
    T = G.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        String text = currentTime.ToString("#.0");
        T.text = text;

        if(death)
            SceneManager.LoadScene("LevelZero");


    }
}
