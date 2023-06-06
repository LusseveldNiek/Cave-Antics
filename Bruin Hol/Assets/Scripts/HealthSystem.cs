using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public GameObject[] hearts;
    private float damageTimer;
    public float damageWaitTime;
    public bool canDoDamage;
    

    void Update()
    {
        if(hearts[2].activeInHierarchy == false)
        {
            print("gameOver");
        }

        if(canDoDamage == false)
        {
            damageTimer += Time.deltaTime;
            if(damageTimer > damageWaitTime)
            {
                canDoDamage = true;
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
                    break;
                }
            }
        }
    }

    private void OnCollisionEnter(Collider collision)
    {
        if (collision.gameObject.tag == "doesDamage")
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
}
