using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class script_Button : MonoBehaviour
{
    
    // Start is called before the first frame update
    public void NextScene()
    {
        SceneManager.LoadScene("LevelZero");
    }
}
