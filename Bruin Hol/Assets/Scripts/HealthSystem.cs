using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public GameObject[] hearts;
    private float damageTimer;
    public float damageWaitTime;
    public bool canDoDamage;
    public Animator animator;
    public float speed;
    public GameObject gameOverCanvas;
    public Button button;
    public bool gettingDamage;

    private float gameOverTime;
    public float gameOverTimeLimit;

    public Renderer playerRenderer; // Reference to the player's renderer component
    public UnityEngine.Material blinkingMaterial; // Material to be used when blinking
    public float blinkInterval = 0.3f; // Interval between blinks

    public int overlappingColliders = 0;

    private void Start()
    {
        
    }

    void Update()
    {
        GetComponent<MovementPlayer>().canDoDamage = canDoDamage;

        if(hearts[2].activeInHierarchy == false && canDoDamage == false)
        {
            print("gameOver");
            animator.SetBool("isDead", true);

            gameOverTime += Time.deltaTime;
            if (gameOverTime > gameOverTimeLimit)
            {
                gameOverCanvas.SetActive(true);
                button.Select();
                GetComponent<MovementPlayer>().enabled = false;
            }
        }

        if(canDoDamage == false)
        {
            animator.SetBool("playerDamage", true);
            damageTimer += Time.deltaTime;
            gettingDamage = true;
            StartCoroutine(BlinkCoroutine());
            if (damageTimer > damageWaitTime)
            {
                gettingDamage = false;
                canDoDamage = true;
                animator.SetBool("playerDamage", false);
                damageTimer = 0;
            }
        }
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "doesDamage")
        {
            for(int i = 0; i < hearts.Length; i++)
            {
                if(hearts[i].gameObject.activeInHierarchy && canDoDamage)
                {
                    hearts[i].SetActive(false);
                    canDoDamage = false;
                    GetComponent<Rigidbody>().AddForce(Vector3.right * (transform.position.x - other.transform.position.x) * speed);
                    break;
                }
            }
        }

        if(other.gameObject.tag == "stenenRechthoek")
        {
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "doesDamage")
        {
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
