using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CameraControls : MonoBehaviour
{
    public Vector3 save;
    float timer, intense, total;

    float streakTimer, streakTotal;

    public RawImage streak;
    public GameObject gm;

    // Start is called before the first frame update
    void Start()
    {
        save = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            transform.position = save + new Vector3(Random.insideUnitCircle.x, Random.insideUnitCircle.y, 0) * intense * (timer / total);
        }
        else
        {
            transform.position = save;
        }

        if (streakTimer > 0)
        {
            streakTimer -= Time.deltaTime;
            streak.color = new Color(1, 1, 1, (timer / total) * .8f);
        }
        else
        {
            streak.color = Color.clear;
        }

        if(transform.position.x ==80)
        {
            gm.GetComponent<script_GameManager>().win = true;
        }
    }

    public void CameraShake(float intensity, float t)
    {
        total = t;
        timer = t;
        intense = intensity;
    }

    public void StreakLines(float t)
    {
        streak.color = new Color(1, 1, 1, .8f);
        streakTotal = t;
        streakTimer = t;
    }
}
