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

    public bool hasSulfur;
    public bool hasCoal;

    void Update()
    {
        //sulfur == yellow & coal == black

        foreach(GameObject material in objectsInInventory)
        {
            if(material != null)
            {
                if (material.tag == "Sulfur")
                {
                    for (int i = 0; i < inventory.Length; i++)
                    {
                        if (inventory[i].texture == null && hasSulfur == false)
                        {
                            Color imageColor = inventory[i].color;
                            imageColor.a = 255;
                            inventory[i].color = imageColor;

                            inventory[i].texture = sulfur;
                            print("sulfurCollected");
                            hasSulfur = true;
                            break;
                        }
                    }
                }

                else if (material.tag == "Coal")
                {
                    for (int i = 0; i < inventory.Length; i++)
                    {
                        if (inventory[i].texture == null && hasCoal == false)
                        {
                            Color imageColor = inventory[i].color;
                            imageColor.a = 255;
                            inventory[i].color = imageColor;

                            inventory[i].texture = coal;
                            print("coalCollected");
                            hasCoal = true;
                            break;
                        }
                    }
                }
            }

        }
    }
}
