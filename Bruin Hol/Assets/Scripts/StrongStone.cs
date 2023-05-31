using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongStone : MonoBehaviour
{
    public GameObject inventory;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "pickaxe")
        {
            bool hasCoal = inventory.GetComponent<Inventory>().hasCoal;
            bool hasSulfur = inventory.GetComponent<Inventory>().hasSulfur;
            if(hasSulfur && hasCoal)
            {
                Destroy(gameObject);
            }
        }
    }
}
