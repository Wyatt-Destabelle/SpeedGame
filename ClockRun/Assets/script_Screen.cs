using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_Screen : MonoBehaviour
{
    public bool moving;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(moving)
            transform.position += Vector3.up*Time.fixedDeltaTime*25;

        if(transform.position.y > 17)
        {
            moving = false;
            transform.position = new Vector3(transform.position.x,-17,transform.position.z);
        }

    }
}
