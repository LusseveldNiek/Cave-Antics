using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public bool gameOver;

    public GameObject[] hearts;
    private float damageTimer;
    public float damageWaitTime;
    public bool canDoDamage;
    public float speed;
    public bool gettingDamage;

    private float gameOverTime;
    public float gameOverTimeLimit;

    public GameObject crushingObject;
    private bool crushedByCrusherObject;

    private bool isGrounded;
    public Vector3 lastGroundedPosition;
    private float previousTime;
    public bool inSpike;
    private RaycastHit hit;
    public Transform bottomPlayer;

    [Header("UI")]
    public Renderer playerRenderer; // Reference to the player's renderer component
    public UnityEngine.Material blinkingMaterial; // Material to be used when blinking
    public float blinkInterval = 0.3f; // Interval between blinks

    public Animator animator;
    public GameObject gameOverCanvas;
    public Button button;



    private void Start()
    {
        
    }

    void Update()
    {
        GetComponent<MovementPlayer>().canDoDamage = canDoDamage;

        //game over
        if(hearts[2].activeInHierarchy == false && canDoDamage == false)
        {
            print("gameOver");
            animator.SetBool("isDead", true);
            GetComponent<MovementPlayer>().enabled = false;
            gameOver = true;
        }

        if(gameOver)
        {
            gameOverTime += Time.deltaTime;
            if (gameOverTime > gameOverTimeLimit)
            {
                gameOverCanvas.SetActive(true);
                button.Select();
            }
        }

        //player damage timeout
        if(canDoDamage == false)
        {
            animator.SetBool("playerDamage", true);
            damageTimer += Time.deltaTime;
            gettingDamage = true;
            StartCoroutine(BlinkCoroutine());
            if(crushingObject != null)
            {
                crushingObject.transform.parent.gameObject.GetComponent<Collider>().enabled = false;
                crushedByCrusherObject = true;
            }

            if (damageTimer > damageWaitTime)
            {
                gettingDamage = false;
                canDoDamage = true;
                animator.SetBool("playerDamage", false);
                damageTimer = 0;
                if(crushedByCrusherObject)
                {
                    crushingObject.transform.parent.gameObject.GetComponent<Collider>().enabled = true;
                    crushingObject = null;
                    crushedByCrusherObject = false;
                }
                
            }
        }
        
        //check if grounded
        Physics.Raycast(bottomPlayer.position, -transform.up, out hit, 1);
        if(hit.transform != null)
        {
            
            if(hit.transform.gameObject.GetComponent<Rigidbody>() == null && hit.transform.gameObject.tag == "Ground")
            {
                isGrounded = true;
            }

            else
            {
                isGrounded = false;
            }
        }

        else
        {
            isGrounded = false;
        }
            
        
        if (isGrounded)
        {
            // Update the last known grounded position
            if (Time.time - previousTime >= 1f)
            {
                lastGroundedPosition = transform.position;
                previousTime = Time.time;
            }

        }

        
    }

    private void FixedUpdate()
    {
        //remove player from spike
        if (inSpike)
        {
            StartCoroutine(BlinkCoroutine());
            transform.position = Vector3.Lerp(transform.position, lastGroundedPosition, 4 * Time.deltaTime);
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<MovementPlayer>().enabled = false;
            if (transform.position == lastGroundedPosition)
            {
                GetComponent<BoxCollider>().enabled = true;
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<MovementPlayer>().enabled = true;
                inSpike = false;
            }
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if(gameOver == false)
        {
            if (other.gameObject.tag == "doesDamage")
            {
                //check health
                for (int i = 0; i < hearts.Length; i++)
                {
                    if (hearts[i].gameObject.activeInHierarchy && canDoDamage)
                    {
                        hearts[i].SetActive(false);
                        canDoDamage = false;
                        GetComponent<Rigidbody>().AddForce(Vector3.right * (transform.position.x - other.transform.position.x) * speed);
                        if (other.isTrigger)
                        {
                            crushingObject = other.gameObject;
                        }
                        break;
                    }
                }
            }

            if (other.gameObject.tag == "stenenRechthoek")
            {
                //check health
                for (int i = 0; i < hearts.Length; i++)
                {
                    if (hearts[i].gameObject.activeInHierarchy && canDoDamage)
                    {
                        hearts[i].SetActive(false);
                        canDoDamage = false;
                        break;
                    }
                }
            }
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "doesDamage")
        {
            //check if object touches spike
            if (collision.gameObject.GetComponent<RespawnGround>() != null)
            {
                inSpike = true;
            }

            //check health
            for (int i = 0; i < hearts.Length; i++)
            {
                if (hearts[i].gameObject.activeInHierarchy && canDoDamage)
                {
                    hearts[i].SetActive(false);
                    canDoDamage = false;
                    GetComponent<Rigidbody>().AddForce(Vector3.right * (transform.position.x - collision.transform.position.x) * speed);
                    break;
                }
            }


            
        }
    }

    private IEnumerator BlinkCoroutine()
    {
        // Continuously blink every blinkInterval seconds
        while (gettingDamage)
        {
            // Swap to blinking material
            playerRenderer.GetComponent<SkinnedMeshRenderer>().enabled = false;

            // Wait for the specified interval
            yield return new WaitForSeconds(blinkInterval);

            // Swap back to the original material
            playerRenderer.GetComponent<SkinnedMeshRenderer>().enabled = true;

            // Wait for the specified interval
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
