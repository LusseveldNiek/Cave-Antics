using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    [Header("Player")]
    public float speedPlayer;
    public float jumpForce; 
    public float fallSpeed;
    public Rigidbody playerRigidbody;

    public GameObject sprintParticle;
    private bool isNotSpawning;

    [Header("Jumping")]
    public bool isGrounded;
    private RaycastHit groundHit;
    public float height; //hoogte tussen speler en grond

    [Header("Sprinting")]
    public float sprintSpeed; //sprint snelheid
    public float fastSprintSpeed; //fast sprint snelheid
    private float sprintCounter; // speler wordt langzaam sneller
    private float beginPlayerSpeed; // reset snelheid speler
    private float sprintMultiplier; //voor andere sprint fase verdubbelaar
    private float sprintTimer; //timer hoelang sprint fase
    public float maxSprintSpeed;

    [Header("WallJumping")]
    private RaycastHit wallRight;
    private RaycastHit wallLeft;
    public float slideSpeed;
    private bool isOnWall;

    [Header("Attacking")]
    public int damage;

    

    void Start()
    {
        beginPlayerSpeed = speedPlayer;
    }

    void Update()
    {
        if(isOnWall == false)
        {
            //horizontal movement
            transform.Translate(Input.GetAxis("Horizontal") * speedPlayer * Time.deltaTime, 0, 0);
        }
        
        IncreaseMass();
        Sprinting();
        WallJumping();
        Attacking();
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
        //maakt springen korter
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

    void WallJumping()
    {

        Physics.Raycast(transform.position, -transform.right, out wallLeft, 0.5f);
        Physics.Raycast(transform.position, transform.right, out wallRight, 0.5f);

        if(wallLeft.transform != null)
        {
            if(wallLeft.transform.gameObject.tag == "wall")
            {
                playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, -slideSpeed, playerRigidbody.velocity.z);
                isOnWall = true;
            }
        }
        else if (wallRight.transform != null)
        {
            if(wallRight.transform.gameObject.tag == "wall")
            {
                playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, -slideSpeed, playerRigidbody.velocity.z);
                isOnWall = true;
            }
        }

        else
        {
            isOnWall = false;
        }
    }

    void Sprinting()
    {
        //sprinten
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speedPlayer += sprintCounter * sprintCounter * sprintMultiplier;
            sprintCounter += sprintSpeed * Time.deltaTime;

            sprintTimer += Time.deltaTime;
            if(sprintTimer > 0.3f)
            {
                sprintMultiplier = fastSprintSpeed;
                //print("sprintingFast");


                //sprint particle
                GameObject particle = Instantiate(sprintParticle, transform.position, Quaternion.identity);
                Destroy(particle, 1);
            }
        }

        else
        {
            speedPlayer = beginPlayerSpeed;
            sprintCounter = 0;
            //print("reset");
            sprintTimer = 0;
            sprintMultiplier = 1;
        }

        speedPlayer = Mathf.Clamp(speedPlayer, 0, maxSprintSpeed);
    }

    void Attacking()
    {
        if(Input.GetKey(KeyCode.E))
        {

        }
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
