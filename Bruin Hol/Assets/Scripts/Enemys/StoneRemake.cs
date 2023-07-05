using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneRemake : MonoBehaviour
{
    public float maximumSpeed;
    private bool isRolling;
    public GameObject player;
    
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

        if(Vector3.Distance(player.transform.position, transform.position) < 4)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<BoxCollider>().enabled = false;
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
