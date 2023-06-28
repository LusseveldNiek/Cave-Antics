using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBullet : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public Rigidbody rb;

    public void Update()
    {
        if (rb.velocity.magnitude > 0.1f) // Check if the object is moving
        {
            Vector3 direction = rb.velocity.normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
    // Shoot the bullet in the specified direction
    public void ShootInDirection(Vector3 direction)
    {
        // Set the initial velocity of the bullet
        GetComponent<Rigidbody>().velocity = direction * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag != "flower")
        {
            Destroy(gameObject);
        }
    }
}
