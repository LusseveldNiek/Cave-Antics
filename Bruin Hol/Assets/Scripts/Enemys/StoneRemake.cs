using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneRemake : MonoBehaviour
{
    public float maximumSpeed;
    private bool isRolling;
    
    private void Update()
    {
        if(GetComponent<Rigidbody>().velocity.magnitude > maximumSpeed)
        {
            isRolling = true;
        }

        if(isRolling)
        {
            if(GetComponent<Rigidbody>().IsSleeping())
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "doesDamage")
        {
            Physics.IgnoreCollision(GetComponent<CapsuleCollider>(), collision.gameObject.GetComponent<Collider>());
        }
    }
}
