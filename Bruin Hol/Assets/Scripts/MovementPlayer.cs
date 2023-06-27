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
    public Animator animator;
    public GameObject playerMesh;

    [Header("Jumping")]
    public bool isGrounded;
    private RaycastHit groundHit;
    public float height; //hoogte tussen speler en grond
    public GameObject groundParticle;
    public Transform bottomPlayer;

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
    public float slowDownSliding;

    public float crouchSpeed;

    [Header("SlopeRotation")]
    public float slopeRotationSpeed;

    [Header("pickaxe")]
    public GameObject pickaxe;

    [Header("Extra")]
    public bool gameStarted;
    public GameObject startButton;
    public bool menuActive;
    public bool canDoDamage;
    public float playerRotationSpeed;

    Vector3 movementDirection; // Calculate the movement direction based on player input
    float movementSpeed = 5f; // Set the movement speed (replace 5f with your desired speed value)

    float distance;


    void Start()
    {
        beginPlayerSpeed = speedPlayer;
        beginRollingSpeed = rollingSpeed;

        distance = movementSpeed * Time.deltaTime;
    }

    void Update()
    {
        //pickaxe following player
        pickaxe.transform.position = transform.position;

        //player cannot move backward or forward
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        if (!isOnRightWall && !isOnLeftWall && !rightWallJumping && !leftWallJumping && gameStarted && !menuActive && canDoDamage)
        {
            //horizontal movement
            

            float movement = Input.GetAxis("Horizontal") * speedPlayer * Time.deltaTime;
            RaycastHit hit;
            Vector3 movementDirection = new Vector3(movement, 0, 0);
            if (Physics.Raycast(transform.position, movementDirection, out hit, movementDirection.magnitude))
            {
                // Adjust position to the point of collision
                transform.position = hit.point;
            }
            else
            {
                // Move the player
                transform.Translate(movementDirection);
            }

            //walking animation
            if (Input.GetAxis("Horizontal") != 0)
            {
                animator.SetBool("playerWalking", true);
                if(Input.GetAxis("Horizontal") > 0)
                {
                    animator.speed = Input.GetAxis("Horizontal");
                }

                else if(Input.GetAxis("Horizontal") < 0)
                {
                    float positiveNumberSpeed = Mathf.Abs(Input.GetAxis("Horizontal"));
                    animator.speed = positiveNumberSpeed; //negative number speed
                }
            }

            else
            {
                animator.speed = 1;
                animator.SetBool("playerWalking", false);
            }
        }
       

        //check if game started
        if (gameStarted == false)
        {
            gameStarted = startButton.GetComponent<StartButton>().startTimer;
        }
        
        IncreaseMass();
        Sprinting();
        WallJumping();
        RollingAndCrouching();
        SlopeRotation();

        //speler kan niet roteren wanneer wallJumping
        if(!isOnLeftWall && !isOnRightWall)
        {
            RotatePlayerMesh();
        }
    }

    void FixedUpdate()
    {
        //jumping
        if (Gamepad.all[0].buttonSouth.isPressed && isGrounded && gameStarted && !menuActive)
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce);
            animator.SetBool("isJumping", true);
        }
    
        //jumping from right wall
        if (Gamepad.all[0].buttonSouth.isPressed && isOnRightWall)
        {
            rightWallJumping = true;
            animator.SetBool("wallJumping", true);
            isOnRightWall = false;
            playerRigidbody.AddForce(-Vector3.right * wallJumpSideForce);
            playerRigidbody.AddForce(Vector3.up * wallJumpUpForce);
        }

        //jumping from left wall
        if (Gamepad.all[0].buttonSouth.isPressed && isOnLeftWall)
        {
            leftWallJumping = true;
            animator.SetBool("wallJumping", true);
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

        // limit player fall speed
        height = Mathf.Clamp(height, 0, 4);
        if (isGrounded == false)
        {
            Physics.Raycast(bottomPlayer.position, -transform.up, out groundHit, 100);          
            Physics.gravity = new Vector3(0, -height * fallSpeed, 0);
        }

        else
        {
            Physics.gravity = new Vector3(0, -10, 0);
        }
    }

    void WallJumping()
    {
        if(isOnRightWall && rightWallJumping)
        {
            rightWallJumping = false;
            animator.SetBool("wallJumping", false);
        }

        else if(isOnLeftWall && leftWallJumping)
        {
            animator.SetBool("wallJumping", false);
            leftWallJumping = false;
        }

        bool raycastLeftHit = Physics.Raycast(transform.position, -transform.right, out wallLeft, 0.5f);
        bool raycastRightHit = Physics.Raycast(transform.position, transform.right, out wallRight, 0.5f);
        //player hitting wall
        if (raycastLeftHit)
        {
            if(wallLeft.collider.CompareTag("wall"))
            {
                isOnLeftWall = true;
                print("onWall");
            }
        }

        else
        {
            isOnLeftWall = false;
            animator.SetBool("onWall", true);
        }

        if (raycastRightHit)
        {
            if(wallRight.collider.CompareTag("wall"))
            {
                isOnRightWall = true;
                print("onWall");
            }
        }

        else
        {
            isOnRightWall = false;
            animator.SetBool("onWall", false);
        }

        if(isGrounded)
        {
            animator.SetBool("wallJumping", false);
            leftWallJumping = false;
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
            animator.SetBool("onWall", true);

            Quaternion target = Quaternion.Euler(playerMesh.transform.rotation.x, 90, playerMesh.transform.rotation.z);

            // rotate player when turning
            playerMesh.transform.localRotation = target;
        }

        if(isOnRightWall)
        {
            print("enteredRight");
            leftWallJumping = false;

            Vector3 vel = playerRigidbody.velocity;
            vel.y = wallHangSpeed;
            playerRigidbody.velocity = vel;
            transform.rotation = Quaternion.identity;
            animator.SetBool("onWall", true);

            Quaternion target = Quaternion.Euler(playerMesh.transform.rotation.x, -90, playerMesh.transform.rotation.z);

            // rotate player when turning
            playerMesh.transform.localRotation = target;
        }
    }

    void Sprinting()
    {
        //sprinten
        if (Gamepad.all[0].rightTrigger.ReadValue() > 0)
        {
            animator.SetBool("isRunning", true);
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
            animator.SetBool("isRunning", false);
        }

        speedPlayer = Mathf.Clamp(speedPlayer, 0, maxSprintSpeed);
    }

    void RollingAndCrouching()
    {
        //isRolling
        if (Gamepad.all[0].buttonEast.ReadValue() > 0 && sprinting)
        {
            isRolling = true;
            animator.SetBool("isSliding", true);
            if (transform.rotation.z > 1)
            {
                //goingLeft
                transform.Translate(Vector3.left * (transform.rotation.z * speedPlayer) / slowDownSliding);
                speedPlayer = beginPlayerSpeed * 2;
            }

            else if (transform.rotation.z < 1)
            {
                //goingRight
                float positiveNumber = Mathf.Abs((transform.rotation.z * speedPlayer) / slowDownSliding);
                transform.Translate(Vector3.right * positiveNumber);
                speedPlayer = beginPlayerSpeed * 2;
            }

            speedPlayer -= rollingSpeed * rollingSpeed;
            rollingSpeed += rollingSpeed * Time.deltaTime;

            speedPlayer = Mathf.Clamp(speedPlayer, 0, 7);
        }

        else
        {
            isRolling = false;
            rollingSpeed = beginRollingSpeed;
            animator.SetBool("isSliding", false);
        }

        //crouching
        if (Gamepad.all[0].buttonEast.ReadValue() > 0 && isRolling == false)
        {
            speedPlayer = crouchSpeed;
            animator.SetBool("isCrouching", true);
        }

        else
        {
            animator.SetBool("isCrouching", false);
        }

        
    }

    void RotatePlayerMesh()
    {
        float rotationAxis = Input.GetAxis("Horizontal");
        if (rotationAxis > 0)
        {

            Quaternion target = Quaternion.Euler(playerMesh.transform.rotation.x, 90, playerMesh.transform.rotation.z);

            // rotate player when turning
            playerMesh.transform.localRotation = target;
        }

        if (rotationAxis < 0)
        {

            Quaternion target = Quaternion.Euler(playerMesh.transform.rotation.x, -90, playerMesh.transform.rotation.z);

            // rotate player when turning
            playerMesh.transform.localRotation = target;
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
            animator.SetBool("isJumping", false);
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
