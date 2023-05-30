using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMenu : MonoBehaviour
{
    public GameObject[] menu;
    public bool menuIsActive;
    public GameObject player;

    private void Update()
    {
        player.GetComponent<MovementPlayer>().menuActive = menuIsActive;
        foreach (GameObject obj in menu)
        {
            // If any object is active, set the bool to true and break the loop
            if (obj.activeSelf)
            {
                menuIsActive = true;
                break;
            }

            else
            {
                menuIsActive = false;
            }
        }
    }
}
