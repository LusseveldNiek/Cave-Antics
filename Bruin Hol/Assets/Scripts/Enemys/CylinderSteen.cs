using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderSteen : MonoBehaviour
{
    private RaycastHit hitRight;
    private RaycastHit hitLeft;
    public float speed;

    public bool goingLeft;
    public bool goingRight;

    public float raycastDistance;
    public float maxDeceleration;
    public float stoppingDistance = 1f;

    public float targetDistance;
    public float distance;

    public void Update()
    {
        /*
        if(goingLeft)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.left * speed * Time.deltaTime);
            if (hitLeft.transform != null)
            {
                distance = hitLeft.distance;

                targetDistance = distance - stoppingDistance;

                if (targetDistance <= 1)
                {
                    // The object is already within the stopping distance, so stop it
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    print("wauw");
                    hitLeft = new RaycastHit();
                    goingRight = true;
                    goingLeft = false;
                    return;
                    
                }

                // Calculate the deceleration based on the target distance
                float deceleration = Mathf.Lerp(0f, maxDeceleration, (raycastDistance - targetDistance) / raycastDistance);

                // Apply deceleration to slow down the object
                Vector3 currentVelocity = GetComponent<Rigidbody>().velocity;
                Vector3 decelerationVector = currentVelocity.normalized * -deceleration;
                GetComponent<Rigidbody>().AddForce(decelerationVector, ForceMode.Acceleration);
            }
        }

        if (goingRight)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.right * speed * Time.deltaTime);
            if (hitRight.transform != null)
            {
                float distance = hitRight.distance;

                float targetDistance = distance - stoppingDistance;

            if (targetDistance <= 1)
            {
                    // The object is already within the stopping distance, so stop it
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    hitRight = new RaycastHit();
                    goingLeft = true;
                    goingRight = false;
                    return;
            }

                // Calculate the deceleration based on the target distance
                float deceleration = Mathf.Lerp(0f, maxDeceleration, (raycastDistance - targetDistance) / raycastDistance);

                // Apply deceleration to slow down the object
                Vector3 currentVelocity = GetComponent<Rigidbody>().velocity;
                Vector3 decelerationVector = currentVelocity.normalized * -deceleration;
                GetComponent<Rigidbody>().AddForce(decelerationVector, ForceMode.Acceleration);
            }
        }

        Physics.Raycast(transform.position + new Vector3(0, 1, 0), Vector3.right, out hitRight, raycastDistance);
        Physics.Raycast(transform.position + new Vector3(0, 1, 0), Vector3.left, out hitLeft, raycastDistance);
        */

    }

    
}
