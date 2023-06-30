using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StekelOpGrond : MonoBehaviour
{
    public float force;
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Vector3 forceDirection = (Vector3.up + Vector3.left).normalized;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(forceDirection * force, ForceMode.Force);
        }
    }
}
