using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class script_batBehavior : MonoBehaviour
{
    float dashTimer;
    public float kickForce,hitForce;
    Transform batTransform;
    Rigidbody2D playerRB;
    bool swinging,kickAllowed;
    int swingTimer;

    Camera cam;
    CameraControls camControls;

    public ParticleSystem hitWallPS;
    public ParticleSystem hitGearPS;

    public AudioSource
        whiffSFX,
        hitGearSFX,
        hitWallSFX;

    Animator animationController;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider2D>().enabled = false;
        playerRB = GetComponentInParent<Rigidbody2D>();
        batTransform = GetComponent<Transform>();
        animationController = GetComponentInParent<Animator>();
        swingTimer = 0;
        kickAllowed = true;

        cam = Camera.main;
        camControls = cam.GetComponent<CameraControls>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.right = new Vector3(mousePos.x,mousePos.y,transform.position.z) - transform.position;

        GetComponentInParent<script_playerMovement>().lookDir = transform.position.x - mousePos.x >= 0f;

        if (Input.GetMouseButtonDown(0) && swingTimer == 0)
        {
            whiffSFX.Play();
            swinging = true;
            Time.timeScale = .5f;

            animationController.SetTrigger("StopAnim");


        }
        if(Input.GetMouseButtonDown(0) && swingTimer == 0)
        {
                                    animationController.SetBool("Swinging",true);
            float angle = Vector2.SignedAngle(Vector2.right,transform.right);
            if(angle < 22.5 && angle >= -22.5)
                animationController.SetInteger("SwingType",2);
            else if(angle < -22.5 && angle >= -67.5)
                animationController.SetInteger("SwingType",1);
            else if(angle < -67.5 && angle >= -102.5)
                animationController.SetInteger("SwingType",0);
            else if(angle < 67.5 && angle >= 22.5)
                animationController.SetInteger("SwingType",3);
            else if(angle < 102.5 && angle > 67.5)
                animationController.SetInteger("SwingType",4);
            Time.timeScale = 1;
            GetComponent<BoxCollider2D>().enabled = true;
            kickAllowed = true;
            swingTimer = 10;
        }

        
    }
    void FixedUpdate()
    {
        if(dashTimer > 0)
        {
            dashTimer -= 1;
            if(dashTimer == 0)
            {
                            GetComponentInParent<script_playerMovement>().movementLockout = false;
            }

        }
        if(swingTimer > 0)
        {
            animationController.SetBool("Swinging", false);
            swingTimer -= 1;
            if(swingTimer == 0)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                swinging = false;
                
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if((col.gameObject.tag.Equals("Ground")||col.gameObject.tag.Equals("Wall")) && kickAllowed)
        {
            kickAllowed = false;

            hitWallSFX.Play();

            if(transform.right.y < 0)
            GetComponentInParent<script_playerMovement>().grounded = false;
            GetComponentInParent<script_playerMovement>().movementLockout = true;
            dashTimer = 5;

            swingTimer = 0;
            GetComponent<BoxCollider2D>().enabled = false;
            swinging = false;

            playerRB.velocity = Vector2.zero;
            playerRB.AddForce(transform.right * -kickForce,ForceMode2D.Impulse);

            hitWallPS.transform.position = col.ClosestPoint(batTransform.position);
            hitWallPS.Play();

            camControls.CameraShake(.05f, .2f);
        }
        if(col.gameObject.tag.Equals("Gear"))
        {
            col.GetComponent<Rigidbody2D>().AddForce(transform.right*hitForce,ForceMode2D.Impulse);

            hitGearSFX.Play();

            swingTimer = 0;
            GetComponent<BoxCollider2D>().enabled = false;
            swinging = false;

            StartCoroutine("HitStop");

            playerRB.AddForce(transform.right * -kickForce * .5f, ForceMode2D.Impulse);

            hitGearPS.transform.position = col.ClosestPoint(batTransform.position);
            hitGearPS.Play();

            camControls.CameraShake(.15f, .4f);
            //camControls.StreakLines(.1f);
        }

    }

    public IEnumerator HitStop()
    {
        yield return new WaitForSecondsRealtime(.01f);
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(.1f);
        camControls.StreakLines(.2f);
        Time.timeScale = 1f;
    }
}
