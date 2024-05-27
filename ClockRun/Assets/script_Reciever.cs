using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class script_Reciever : MonoBehaviour
{
    float MoveTime;

    public AudioSource lockSFX;

    // Start is called before the first frame update
    void Start()
    {
        MoveTime = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(MoveTime > 0)
        {
            transform.position = transform.position + 18.5f*Vector3.up*Time.fixedDeltaTime;
            MoveTime -= 1;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Gear")
        {
            lockSFX.Play();
            Destroy(col);
            MoveTime = 10;
        }
    }
}
