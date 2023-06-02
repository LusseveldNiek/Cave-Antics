using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public float forceUp;
    public float forceSide;
    private bool isGrounded;

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

            isGrounded = other.GetComponent<MovementPlayer>().isGrounded;

            if(isGrounded)
            {
                if (other.transform.position.x < transform.position.x)
                {
                    Debug.Log("Player is on the left side of the mushroom.");
                    Vector3 diagonalForce = new Vector3(5, 5, 0f);
                    other.GetComponent<Rigidbody>().AddForce(diagonalForce * forceSide, ForceMode.Impulse);
                }

                else
                {
                    Debug.Log("Player is on the right side of the mushroom.");
                    Vector3 diagonalForce = new Vector3(-5, 5, 0f);
                    other.GetComponent<Rigidbody>().AddForce(diagonalForce * forceSide, ForceMode.Impulse);
                }
            }

            else
            {
                // Apply the throw by adding force to the object
                rb.AddForce(throwDirection * speed * forceUp, ForceMode.Impulse);
            }
        }
    }
}
