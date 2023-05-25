using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public float force;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
           // Check the speed of the object that came close
            float speed = rb.velocity.magnitude;

            // Get the direction from the collision point to the sphere's center
            Vector3 collisionDirection = transform.position - other.transform.position;

            // Calculate the mirrored direction based on the x-axis of the sphere
            Vector3 throwDirection = new Vector3(-collisionDirection.x, -collisionDirection.y, 0).normalized;

            // Apply the throw by adding force to the object
            rb.AddForce(throwDirection * speed * force, ForceMode.Impulse);
        }
    }
}
