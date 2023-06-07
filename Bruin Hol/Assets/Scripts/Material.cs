using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material : MonoBehaviour
{
    public GameObject inventory;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "pickaxe")
        {
            inventory.GetComponent<Inventory>().objectsInInventory.Add(gameObject);
            print("material");
        }

        if (other.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(other.GetComponent<BoxCollider>(), GetComponent<BoxCollider>());
        }
    }
}
