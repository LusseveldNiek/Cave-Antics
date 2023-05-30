using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> objectsInInventory;
    public List<GameObject> inventory;

    void Update()
    {
        //sulfur == yellow & coal == black

        foreach(GameObject material in objectsInInventory)
        {
            if(material.tag == "Sulfur")
            {

            }

            else if (material.tag == "Coal")
            {

            }
        }
    }
}
