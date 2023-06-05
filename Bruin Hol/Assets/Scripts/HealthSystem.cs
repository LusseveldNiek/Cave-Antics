using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public GameObject[] hearts;
    

    void Update()
    {
        if(hearts[2].activeInHierarchy == false)
        {
            print("gameOver");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "flowerBullet" || other.gameObject.tag == "cactus")
        {
            for(int i = 0; i < hearts.Length; i++)
            {
                if(hearts[i].gameObject.activeInHierarchy)
                {
                    hearts[i].SetActive(false);
                    break;
                }
            }
        }
    }
}
