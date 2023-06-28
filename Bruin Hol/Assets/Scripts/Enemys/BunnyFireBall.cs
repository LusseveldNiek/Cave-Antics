using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyFireBall : MonoBehaviour
{
    public bool isRight;
    public bool isLeft;
    public float lavaBubbleSideSpeed;
    public float lavaBubbleUpSpeed;


    private void Update()
    {
        Physics.bounceThreshold = 0;

        if(isRight)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.right * lavaBubbleSideSpeed * Time.deltaTime);
        }

        if (isLeft)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.left * lavaBubbleSideSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * lavaBubbleUpSpeed);
        }
    }
}
