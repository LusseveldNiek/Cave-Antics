using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stalactiteTrigger : MonoBehaviour
{
    public Rigidbody stalactite;

    void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.tag == "Player")
        {
            print("Stalactite Sees Player");
            stalactite.isKinematic = false;
            stalactite.useGravity = true;
        }
    }
}
