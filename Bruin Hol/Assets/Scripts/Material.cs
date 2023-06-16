using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material : MonoBehaviour
{
    public GameObject inventory;
    public float diamondScore;
    public GameObject scoreSystem;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "pickaxe")
        {
            inventory.GetComponent<Inventory>().objectsInInventory.Add(gameObject);
            print("material");
            Destroy(gameObject);
        }

        if(other.gameObject.tag == "pickaxe" && gameObject.tag == "diamond")
        {
            scoreSystem.GetComponent<ScoreSystem>().score += diamondScore;
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(other.GetComponent<BoxCollider>(), GetComponent<BoxCollider>());
        }
    }
}
