using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<GameObject> objectsInInventory;
    public RawImage[] inventory;
    public RenderTexture sulfur;
    public RenderTexture coal;

    void Update()
    {
        //sulfur == yellow & coal == black

        foreach(GameObject material in objectsInInventory)
        {
            if(material.tag == "Sulfur")
            {
                for(int i = 0; i < inventory.Length; i++)
                {
                    if (inventory[i].gameObject != null)
                    {
                        inventory[i].texture = sulfur;
                    }
                }
            }

            else if (material.tag == "Coal")
            {
                for (int i = 0; i < inventory.Length; i++)
                {
                    if (inventory[i].gameObject != null)
                    {
                        inventory[i].texture = coal;
                    }
                }
            }
        }
    }
}
