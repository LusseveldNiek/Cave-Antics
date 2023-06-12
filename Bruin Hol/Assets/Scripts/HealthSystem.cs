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
    

    void Update()
    {
        GetComponent<MovementPlayer>().canDoDamage = canDoDamage;

        if(hearts[2].activeInHierarchy == false && canDoDamage == false)
        {
            print("gameOver");
            gameOverCanvas.SetActive(true);
            button.Select();
        }

        if(canDoDamage == false)
        {
            animator.SetBool("playerDamage", true);
            damageTimer += Time.deltaTime;
            if(damageTimer > damageWaitTime)
            {
                canDoDamage = true;
                animator.SetBool("playerDamage", false);
                damageTimer = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "doesDamage")
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
}
