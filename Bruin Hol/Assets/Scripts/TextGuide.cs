using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextGuide : MonoBehaviour
{
    public GameObject collisionArea;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag == "Player")
        {
            collisionArea.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.tag == "Player")
        {
            collisionArea.SetActive(false);
        }
    }
}
