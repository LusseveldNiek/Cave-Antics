using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongStone : MonoBehaviour
{
    public GameObject inventory;
    public GameObject bomb;
    public GameObject bombParticle;
    public Transform bombPosition;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "pickaxe")
        {
            bool hasCoal = inventory.GetComponent<Inventory>().hasCoal;
            bool hasSulfur = inventory.GetComponent<Inventory>().hasSulfur;
            if (hasSulfur && hasCoal)
            {
                inventory.GetComponent<Inventory>().objectsInInventory.Clear();
                for(int i = 0; i < inventory.GetComponent<Inventory>().inventory.Length; i ++)
                {
                    inventory.GetComponent<Inventory>().inventory[i].texture = null;
                }
                inventory.GetComponent<Inventory>().hasCoal = false;
                inventory.GetComponent<Inventory>().hasSulfur = false;
                GameObject prefabBomb = Instantiate(bomb, bombPosition.position, Quaternion.identity);
                Destroy(prefabBomb, 2.5f);
                Destroy(gameObject, 2.5f);
            }
        }
    }
}
