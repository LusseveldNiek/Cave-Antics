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

    [Header("Jumping")]
    public bool isGrounded;
    private RaycastHit groundHit;
    public float height; //hoogte tussen speler en grond
    public GameObject groundParticle;

    [Header("Sprinting")]
    public float sprintSpeed; //sprint snelheid
    public float fastSprintSpeed; //fast sprint snelheid
    private float sprintCounter; // speler wordt langzaam sneller
    private float beginPlayerSpeed; // reset snelheid speler
    public float sprintMultiplier; //voor andere sprint fase verdubbelaar
    private float sprintTimer; //timer hoelang sprint fase
    public float maxSprintSpeed;

    public bool sprinting;
    private bool timerParticleBool;
    private float timerParticle;

    public GameObject sprintParticle;
    public GameObject sprintParticle1;

    public float particleSpawnTime;

    [Header("WallJumping")]
    public float wallJumpSideForce;
    public float wallJumpUpForce;
    private RaycastHit wallRight;
    private RaycastHit wallLeft;

    public float wallHangSpeed;

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

    [Header("pickaxe")]
    public GameObject pickaxe;

    [Header("Extra")]
    public bool gameStarted;
    public GameObject startButton;
    public bool menuActive;
    

    void Start()
    {
        beginPlayerSpeed = speedPlayer;
        beginRollingSpeed = rollingSpeed;
    }

    void Update()
    {
        //pickaxe following player
        pickaxe.transform.position = transform.position;

        //player cannot move backward or forward
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        if (!isOnRightWall && !isOnLeftWall && !rightWallJumping && !leftWallJumping && gameStarted && !menuActive)
        {
            //horizontal movement
            transform.Translate(Input.GetAxis("Horizontal") * speedPlayer * Time.deltaTime, 0, 0);
        }

        //check if game started
        if(gameStarted == false)
        {
            gameStarted = startButton.GetComponent<StartButton>().startTimer;
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
        if (Gamepad.all[0].buttonSouth.isPressed && isGrounded && gameStarted && !menuActive)
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

        //jump particle
        if(height > 1 && height < 1.2)
        {
            GameObject particle = Instantiate(groundParticle, transform.GetChild(0).position, Quaternion.identity);
            Destroy(particle, 1);
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

        bool raycastLeftHit = Physics.Raycast(transform.position, -transform.right, out wallLeft, 0.5f);
        bool raycastRightHit = Physics.Raycast(transform.position, transform.right, out wallRight, 0.5f);
        //player hitting wall
        if (raycastLeftHit)
        {
            if(wallLeft.collider.CompareTag("wall"))
            {
                isOnLeftWall = true;
                print("onWallLeft");
            }
        }

        else
        {
            isOnLeftWall = false;
            leftWallJumping = false;
        }

        if (raycastRightHit)
        {
            if(wallRight.collider.CompareTag("wall"))
            {
                isOnRightWall = true;
                print("onWallright");
            }
        }

        else
        {
            isOnRightWall = false;
            rightWallJumping = false;
        }

        // ik check wanneer de wall jumping klaar is als de speler de grond aan heeft geraakt, maar als de speler een andere muur raakt, is het springen ook klaar
        if (isOnLeftWall)
        {
            print("enteredLeft");
            rightWallJumping = false;

            Vector3 vel = playerRigidbody.velocity;
            vel.y = wallHangSpeed;
            playerRigidbody.velocity = vel;
            transform.rotation = Quaternion.identity;
        }

        if(isOnRightWall)
        {
            print("enteredRight");
            leftWallJumping = false;

            Vector3 vel = playerRigidbody.velocity;
            vel.y = wallHangSpeed;
            playerRigidbody.velocity = vel;
            transform.rotation = Quaternion.identity;
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
            if(sprintTimer > 1 && isGrounded && !playerRigidbody.IsSleeping())
            {
                sprintMultiplier = fastSprintSpeed;
                //print("sprintingFast");


                //sprint particle
                int randomChance = Random.Range(1, 3);
                if(randomChance == 1 && timerParticleBool == false)
                {
                    GameObject particle = Instantiate(sprintParticle, transform.GetChild(0).position, Quaternion.identity);
                    Destroy(particle, 0.3f);
                    timerParticleBool = true;
                    
                }

                if (randomChance == 2 && timerParticleBool == false)
                {
                    GameObject particle = Instantiate(sprintParticle1, transform.GetChild(0).position, Quaternion.identity);
                    Destroy(particle, 0.3f);
                    timerParticleBool = true;
                }

                if (timerParticleBool)
                {
                    timerParticle += Time.deltaTime;
                    if(timerParticle > particleSpawnTime)
                    {
                        timerParticleBool = false;
                        timerParticle = 0;
                    }
                }
            }
        }

        else
        {
            speedPlayer = beginPlayerSpeed;
            sprintCounter = 0;
            //print("reset");
            sprintTimer = 0;
            sprintMultiplier = 0.7f;
            sprinting = false;
            timerParticle = 0;
            timerParticleBool = false;
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

            if (transform.rotation.z > 1)
            {
                //goingLeft
                transform.Translate(Vector3.left * (transform.rotation.z * speedPlayer) / 7);
                speedPlayer = beginPlayerSpeed * 2;
            }

            else if(transform.rotation.z < 1)
            {
                //goingRight
                float positiveNumber = Mathf.Abs((transform.rotation.z * speedPlayer) / 7);
                transform.Translate(Vector3.right * positiveNumber);
                speedPlayer = beginPlayerSpeed * 2;
            }

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            //GameObject particle = Instantiate(groundParticle, transform.GetChild(0).position, Quaternion.identity);
            //Destroy(particle, 1);
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
