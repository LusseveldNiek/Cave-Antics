using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunny : MonoBehaviour
{
    public bool goingLeft;
    public bool goingRight;
    public float speed;

    public float jumpingTime;
    public float jumpingCounter;
    public float jumpSpeed;
    private RaycastHit hit;
    public float raycastDistance;
    public bool isGrounded;

    public GameObject lavaBubble;
    public float lavaBubbleSideSpeed;
    public float lavaBubbleUpSpeed;

    void Update()
    {
        //movement
        if(goingLeft)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if(goingRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }


        //jumping
        if(isGrounded)
        {
            jumpingCounter += Time.deltaTime;
        }
        
        if(jumpingCounter > 1)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpSpeed * Time.deltaTime);
            jumpingCounter = 0;

            if(goingLeft)
            {
                for (int i = 0; i < 2; i++)
                {
                    GameObject lavaBubblePrefab = Instantiate(lavaBubble, transform.position - new Vector3(-1, 0, 0), Quaternion.identity);
                    GetComponent<Rigidbody>().AddForce(Vector3.left * lavaBubbleSideSpeed * Time.deltaTime);
                    GetComponent<Rigidbody>().AddForce(Vector3.up * lavaBubbleUpSpeed * Time.deltaTime);
                }
            }

            if (goingRight)
            {
                for(int i = 0; i < 2; i++)
                {
                    GameObject lavaBubblePrefab = Instantiate(lavaBubble, transform.position - new Vector3(1, 0, 0), Quaternion.identity);
                    GetComponent<Rigidbody>().AddForce(Vector3.right * lavaBubbleSideSpeed * Time.deltaTime);
                    GetComponent<Rigidbody>().AddForce(Vector3.up * lavaBubbleUpSpeed * Time.deltaTime);
                }
            }

        }

        //isGrounded
        Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance);
        if(hit.transform != null)
        {
            if (hit.transform.tag == "Ground")
            {
                isGrounded = true;
            }
        }

        else
        {
            isGrounded = false;
        }
        
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //turning
        if(other.tag == "wall")
        {
            if(other.transform.position.x > transform.position.x)
            {
                //wallOnRight
                goingLeft = true;
                goingRight = false;
            }

            else
            {
                goingRight = true;
                goingLeft = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "doesDamage")
        {
            Physics.IgnoreCollision(GetComponent<BoxCollider>(), collision.gameObject.GetComponent<Collider>());
        }
    }
}
