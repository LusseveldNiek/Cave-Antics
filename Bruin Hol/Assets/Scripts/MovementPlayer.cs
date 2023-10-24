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
    public Transform bottomPlayer;

    public GameObject groundParticle;
    private float particleTimer = 0f;
    private float particleSpawnDelay = 0.5f;

    public float notGroundedTimer;


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
    public GameObject crouchCollider;
    public bool isCrouching;
    public Crouching crouching;

    public float crouchSpeed;
    public RaycastHit hit;

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

    public Vector3 boxSize = Vector3.one;
    public float desiredDistance = 1.0f;
    public float avoidanceForce = 1.0f;
    private float sleepTime;


    void Start()
    {
        beginPlayerSpeed = speedPlayer;
        beginRollingSpeed = rollingSpeed;

        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boxSize);
    }

    void Update()
    {
        

        //player does not clip in walls
        if (isCrouching == false)
        {
            Collider[] colliders = Physics.OverlapBox(transform.position, boxSize / 2.0f, transform.rotation);
            foreach (Collider collider in colliders)
            {
                if (collider.isTrigger) // Check if the collider is a trigger
                {
                    // Ignore trigger colliders
                    continue;
                }

                // Rest of the code for non-trigger colliders
                if (collider.gameObject.CompareTag("Ground") || collider.gameObject.CompareTag("wall") || collider.gameObject.CompareTag("ignoreCollisionPlayer"))
                {
                    // Ignore colliders with specific tags
                    continue;
                }

                // Perform avoidance logic for non-trigger colliders
                Vector3 direction = transform.position - collider.transform.position;
                float distance = direction.magnitude;
                float avoidanceFactor = 1.0f - Mathf.Clamp01(distance / desiredDistance);
                Vector3 avoidanceForceVector = direction.normalized * avoidanceForce * avoidanceFactor;
                transform.position += avoidanceForceVector * Time.deltaTime;
            }
        }
        



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
                speedPlayer = Mathf.Clamp(speedPlayer, 0, 7);
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

        if (!menuActive)
        {
            animator.enabled = true;
        }

        else
        {
            animator.enabled = false;
        }
        
        IncreaseMass();
        Sprinting();
        WallJumping();
        RollingAndCrouching();
        SlopeRotation();

        //speler kan niet roteren wanneer wallJumping
        if(!isOnLeftWall && !isOnRightWall && !menuActive)
        {
            RotatePlayerMesh();
        }

        if(GetComponent<Rigidbody>().IsSleeping())
        {
            sleepTime += Time.deltaTime;
            if(sleepTime > 10)
            {
                animator.SetBool("sitting", true);
                if(sleepTime > 12)
                {
                    animator.SetBool("sitting", false);
                    animator.SetBool("loopSitting", true);
                    if(sleepTime > 20)
                    {
                        animator.SetBool("sleeping", true);
                        animator.SetBool("loopSitting", false);
                        if(sleepTime > 22)
                        {
                            animator.SetBool("loopSleeping", true);
                            animator.SetBool("sleeping", false);
                        }

                    }
                }
            }
        }

        else
        {
            sleepTime = 0;
            animator.SetBool("sitting", false);
            animator.SetBool("loopSitting", false);
            animator.SetBool("Sleeping", false);
            animator.SetBool("loopSleeping", false);
        }
    }

    void FixedUpdate()
    {
        //jumping
        if (Gamepad.all[0].buttonSouth.isPressed && isGrounded && gameStarted && !menuActive && !isRolling && !isCrouching)
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
        particleTimer += Time.deltaTime;
        if (height > 1 && height < 1.2)
        {
            // Check if enough time has passed since the last particle was spawned
            if (particleTimer >= particleSpawnDelay)
            {
                // Spawn a particle
                GameObject particle = Instantiate(groundParticle, transform.GetChild(0).position, Quaternion.identity);
                Destroy(particle, 1);

                // Reset the timer
                particleTimer = 0f;
            }
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
            if(!wallLeft.transform.gameObject.GetComponent<Collider>().isTrigger && wallLeft.collider.CompareTag("wall"))
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
            if(!wallRight.transform.gameObject.GetComponent<Collider>().isTrigger && wallRight.collider.CompareTag("wall"))
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
        if (Gamepad.all[0].rightTrigger.ReadValue() > 0 && GetComponent<Rigidbody>().IsSleeping() == false)
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
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 2))
            {
                if (hit.transform != null)
                {
                    
                    if (hit.transform.gameObject.tag == "Ground")
                    {
                        
                        if (hit.transform.parent != null)
                        {
                            
                            if (hit.transform.parent.gameObject.tag == "leftSliding")
                            {
                                //goingLeft
                                
                                transform.Translate(Vector3.left * (transform.rotation.z * speedPlayer) / slowDownSliding);
                                speedPlayer = beginPlayerSpeed * 2;
                            }

                            else if (hit.transform.parent.gameObject.tag == "rightSliding")
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

                    }
                }
            }
            
            
        }

        else if(crouching.enoughHeightToStopCrouching)
        {
            isRolling = false;
            rollingSpeed = beginRollingSpeed;
            animator.SetBool("isSliding", false);
        }

        //crouching
        if (Gamepad.all[0].buttonEast.ReadValue() > 0 && isRolling == false)
        {
            isCrouching = true;
            speedPlayer = crouchSpeed;
            animator.SetBool("isCrouching", true);
            
        }

        else if(crouching.enoughHeightToStopCrouching)
        {
            isCrouching = false;
            animator.SetBool("isCrouching", false);

        }

        if(isCrouching || isRolling)
        {
            GetComponent<BoxCollider>().enabled = false;
            crouchCollider.GetComponent<BoxCollider>().enabled = true;
        }

        else
        {
            GetComponent<BoxCollider>().enabled = true;
            crouchCollider.GetComponent<BoxCollider>().enabled = false;
            
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

    private void OnCollisionStay(Collision collision)
    {
        if(isCrouching && collision.gameObject.tag == "crouchColliders" || isRolling && collision.gameObject.tag == "crouchColliders")
        {
            Physics.IgnoreCollision(crouchCollider.GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>());
            GetComponent<BoxCollider>().enabled = false;
            crouchCollider.GetComponent<BoxCollider>().enabled = true;
            print("hoi");
        }
    }
}
