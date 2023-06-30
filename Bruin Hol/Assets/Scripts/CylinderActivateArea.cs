using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderActivateArea : MonoBehaviour
{
    public Rigidbody cylinderDude;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag == "Player")
        {
            cylinderDude.useGravity = true;
        }
    }
}
