using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouching : MonoBehaviour
{
    public bool enoughHeightToStopCrouching;

    private void OnTriggerStay(Collider other)
    {
        if(other.transform.gameObject.tag != "Player" && other.transform.gameObject.tag != "pickaxe" && other.transform.gameObject.tag != "TextGuide" && other.transform.gameObject.tag != "Sulfur" && other.transform.gameObject.tag != "Coal")
        {
            enoughHeightToStopCrouching = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.tag != "Player" && other.transform.gameObject.tag != "pickaxe" && other.transform.gameObject.tag != "TextGuide" && other.transform.gameObject.tag != "Sulfur" && other.transform.gameObject.tag != "Coal")
        {
            enoughHeightToStopCrouching = true;
        }
    }
}
