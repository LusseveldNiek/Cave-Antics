using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartAnimation : MonoBehaviour
{
    public GameObject[] hearts;
    public bool damage;

    public void Damage()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i].gameObject.activeInHierarchy)
            {
                hearts[i].SetActive(false);
                hearts[i].GetComponent<Animator>().SetBool("damage", false);
                break;
            }
        }
    }

    public void Update()
    {
        if(damage)
        {

        }
    }
}
