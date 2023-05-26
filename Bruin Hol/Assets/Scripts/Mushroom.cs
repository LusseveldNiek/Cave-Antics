using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public float forceUp;
    public float forceSide;

    private RaycastHit rightSideMushroom;
    private RaycastHit leftSideMushroom;
    public Transform rightSideDirection;
    public Transform leftSideDirection;

    private bool playerIsRightJumping;
    private bool playerIsLeftJumping;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {

        if(Physics.Raycast(transform.position, transform.right, out rightSideMushroom, 1)) //right
        {
            if (rightSideMushroom.transform.gameObject.tag == "Player")
            {
                GetComponent<SphereCollider>().enabled = false;
                rightSideMushroom.transform.GetComponent<Rigidbody>().AddForce(rightSideDirection.forward * forceSide);
                playerIsRightJumping = true;
            }
        }

        if (Physics.Raycast(transform.position, -transform.right, out leftSideMushroom, 1)) //left
        {
            if (leftSideMushroom.transform.gameObject.tag == "Player")
            {
                GetComponent<SphereCollider>().enabled = false;
                leftSideMushroom.transform.GetComponent<Rigidbody>().AddForce(leftSideDirection.forward * forceSide);
            }
        }

        if(playerIsRightJumping)
        {
            if(rightSideMushroom.transform.GetComponent<MovementPlayer>().isGrounded)
            {
                GetComponent<SphereCollider>().enabled = true;
                playerIsRightJumping = false;
            }
        }

        if (playerIsLeftJumping)
        {
            if (leftSideMushroom.transform.GetComponent<MovementPlayer>().isGrounded)
            {
                GetComponent<SphereCollider>().enabled = true;
                playerIsLeftJumping = false;
            }
        }
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
            rb.AddForce(throwDirection * speed * forceUp, ForceMode.Impulse);
        }
    }
}
