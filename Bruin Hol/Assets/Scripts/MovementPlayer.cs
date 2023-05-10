using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    public float speedPlayer, jumpForce, fallSpeed;
    public Rigidbody playerRigidbody;

    public bool isGrounded;
    private RaycastHit groundHit;
    public float height; //hoogte tussen speler en grond

    public float sprintSpeed; //sprint snelheid
    private float sprintCounter; // speler wordt langzaam sneller
    private float beginPlayerSpeed; // reset snelheid speler
    private float sprintMultiplier; //voor andere sprint fase verdubbelaar
    private float sprintTimer; //timer hoelang sprint fase
    public float maxSprintSpeed; 
    void Start()
    {
        beginPlayerSpeed = speedPlayer;
    }

    void Update()
    {
        //horizontal movement
        transform.Translate(Input.GetAxis("Horizontal") * speedPlayer * Time.deltaTime, 0, 0);
        
        //maakt springen korter
        IncreaseMass();
        Sprinting();
    }

    void FixedUpdate()
    {
        //jumping
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce);
        }
    }

    void IncreaseMass()
    {
        height = groundHit.distance;
        if (isGrounded == false)
        {
            Physics.Raycast(transform.position, -transform.up, out groundHit, 100);          
            Physics.gravity = new Vector3(0, -groundHit.distance * fallSpeed, 0);
        }

        else
        {
            Physics.gravity = new Vector3(0, -10, 0);
        }
    }

    void Sprinting()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speedPlayer += sprintCounter * sprintCounter * sprintMultiplier;
            sprintCounter += sprintSpeed * Time.deltaTime;

            sprintTimer += Time.deltaTime;
            if(sprintTimer > 0.3f)
            {
                sprintMultiplier = 3;
                print("sprintingFast");
            }
        }

        else
        {
            speedPlayer = beginPlayerSpeed;
            sprintCounter = 0;
            print("reset");
            sprintTimer = 0;
            sprintMultiplier = 1;
        }

        speedPlayer = Mathf.Clamp(speedPlayer, 0, maxSprintSpeed);


    }

    //is on ground
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
