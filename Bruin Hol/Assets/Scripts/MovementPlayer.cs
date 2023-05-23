using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public bool sprinting;

    [Header("WallJumping")]
    public float wallJumpSideForce;
    public float wallJumpUpForce;
    private RaycastHit wallRight;
    private RaycastHit wallLeft;
    public float slideSpeed;

    public bool isOnRightWall;
    public bool isOnLeftWall;

    public bool rightWallJumping;
    public bool leftWallJumping;

    [Header("Rolling/Crouching")]
    public float rollingSpeed;
    public bool isRolling;
    private float beginRollingSpeed;

    public float crouchSpeed;

    [Header("SlopeRotation")]
    public float slopeRotationSpeed;

    

    void Start()
    {
        beginPlayerSpeed = speedPlayer;
        beginRollingSpeed = rollingSpeed;
    }

    void Update()
    {
        if(isOnRightWall == false && isOnLeftWall == false && rightWallJumping == false && leftWallJumping == false)
        {
            //horizontal movement
            transform.Translate(Input.GetAxis("Horizontal") * speedPlayer * Time.deltaTime, 0, 0);
        }
        
        IncreaseMass();
        Sprinting();
        WallJumping();
        RollingAndCrouching();
        SlopeRotation();
    }

    void FixedUpdate()
    {
        //jumping
        if (Gamepad.all[0].buttonSouth.isPressed && isGrounded)
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce);
        }
    
        //jumping from right wall
        if (Gamepad.all[0].buttonSouth.isPressed && isOnRightWall)
        {
            rightWallJumping = true;
            isOnRightWall = false;
            playerRigidbody.AddForce(-Vector3.right * wallJumpSideForce);
            playerRigidbody.AddForce(Vector3.up * wallJumpUpForce);
        }

        //jumping from left wall
        if (Gamepad.all[0].buttonSouth.isPressed && isOnLeftWall)
        {
            leftWallJumping = true;
            isOnLeftWall = false;
            playerRigidbody.AddForce(Vector3.right * wallJumpSideForce);
            playerRigidbody.AddForce(Vector3.up * wallJumpUpForce);
        }

        //als de speler de grond aanraakt springt de speler niet meer van een muur
        if (isGrounded)
        {
            leftWallJumping = false;
            rightWallJumping = false;
            isOnLeftWall = false;
            isOnRightWall = false;
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
        //player hitting wall
        if (wallLeft.transform != null)
        {
            if(wallLeft.transform.gameObject.tag == "wall" && leftWallJumping == false)
            {
                playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, -slideSpeed, playerRigidbody.velocity.z);
                isOnLeftWall = true;
                print("onWallLeft");
            }
        }

        if (wallRight.transform != null)
        {
            if(wallRight.transform.gameObject.tag == "wall" && rightWallJumping == false)
            {
                playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, -slideSpeed, playerRigidbody.velocity.z);
                isOnRightWall = true;
                print("onWallright");
            }
        }

        // ik check wanneer de wall jumping klaar is als de speler de grond aan heeft geraakt, maar als de speler een andere muur raakt, is het springen ook klaar
        if(isOnLeftWall)
        {
            print("enteredLeft");
            rightWallJumping = false;
        }

        if(isOnRightWall)
        {
            print("enteredRight");
            leftWallJumping = false;
        }

       
    }

    void Sprinting()
    {
        //sprinten
        if (Gamepad.all[0].rightTrigger.ReadValue() > 0)
        {
            sprinting = true;
            speedPlayer += sprintCounter * sprintCounter * sprintMultiplier;
            sprintCounter += sprintSpeed * Time.deltaTime;

            sprintTimer += Time.deltaTime;
            if(sprintTimer > 0.3f)
            {
                sprintMultiplier = fastSprintSpeed;
                //print("sprintingFast");


                //sprint particle
                //GameObject particle = Instantiate(sprintParticle, transform.position, Quaternion.identity);
                //Destroy(particle, 1);
            }
        }

        else
        {
            speedPlayer = beginPlayerSpeed;
            sprintCounter = 0;
            //print("reset");
            sprintTimer = 0;
            sprintMultiplier = 1;
            sprinting = false;
        }

        speedPlayer = Mathf.Clamp(speedPlayer, 0, maxSprintSpeed);
    }

    void RollingAndCrouching()
    {
        //crouching
        if (Gamepad.all[0].buttonEast.ReadValue() > 0)
        {
            speedPlayer = crouchSpeed;
        }

        //isRolling
        if (Gamepad.all[0].buttonEast.ReadValue() > 0 && sprinting)
        {
            isRolling = true;

            speedPlayer -= rollingSpeed * rollingSpeed;
            rollingSpeed += rollingSpeed * Time.deltaTime;

            speedPlayer = Mathf.Clamp(speedPlayer, 0, 1000);
        }

        else
        {
            isRolling = false;
            rollingSpeed = beginRollingSpeed;
        }
    }

    void SlopeRotation()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, -Vector3.up, out hit, 1);
        Vector3 surfaceNormal = hit.normal;

        // Calculate the angle between the surface normal and the player's up vector
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, surfaceNormal) * transform.rotation;

        // Rotate the player towards the surface normal
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * slopeRotationSpeed);
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
