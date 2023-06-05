using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBullet : MonoBehaviour
{
    public float speed;

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
