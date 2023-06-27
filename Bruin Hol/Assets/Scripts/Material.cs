using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material : MonoBehaviour
{
    public GameObject inventory;
    public GameObject scoreSystem;

    //particles
    public GameObject particle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "pickaxe" && gameObject.tag != "diamond")
        {
            inventory.GetComponent<Inventory>().objectsInInventory.Add(gameObject);
            print("material");
            gameObject.SetActive(false);

            if(gameObject.tag == "Coal")
            {
                GameObject particlePrefab = Instantiate(particle, transform.position, Quaternion.identity);
                Destroy(particlePrefab, 4f);
                scoreSystem.GetComponent<ScoreSystem>().coalMining = true;
            }

            else if(gameObject.tag == "Sulfur")
            {
                GameObject particlePrefab = Instantiate(particle, transform.position, Quaternion.identity);
                Destroy(particlePrefab, 4f);
                scoreSystem.GetComponent<ScoreSystem>().sulfurMining = true;
            }
        }

        else if(other.gameObject.tag == "pickaxe" && gameObject.tag == "diamond")
        {
            scoreSystem.GetComponent<ScoreSystem>().diamondMining = true;

            GameObject particlePrefab = Instantiate(particle, transform.position, Quaternion.identity);
            Destroy(particlePrefab, 4f);

            gameObject.SetActive(false); 
        }

        else if (other.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(other.GetComponent<BoxCollider>(), GetComponent<BoxCollider>());
        }
    }
}
