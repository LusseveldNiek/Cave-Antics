using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunny : MonoBehaviour
{
    public bool goingLeft;
    public bool goingRight;

    public bool playSchootAnimation;
    private float schootingTime;
    public float speed;

    public float jumpingTime;
    public float jumpingCounter;
    public float jumpSpeed;
    private RaycastHit hit;
    public float raycastDistance;
    public bool isGrounded;

    public GameObject lavaBubble;
    public GameObject bunnyMesh;
    public Animator animator;

    void FixedUpdate()
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

        if(playSchootAnimation)
        {
            animator.SetBool("schooting", true);
            schootingTime += Time.deltaTime;
            if(schootingTime > 1)
            {
                animator.SetBool("schooting", false);
                playSchootAnimation = false;
                schootingTime = 0;
            }
        }

        //jumping
        if(isGrounded)
        {
            jumpingCounter += Time.deltaTime;
        }
        
        if(jumpingCounter > 1)
        {
            //jumping
            if(jumpingCounter < 1.1f)
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * jumpSpeed * Time.deltaTime);
                animator.SetBool("jumping", true);
            }

            else
            {
                animator.SetBool("jumping", false);
            }

            jumpingCounter += Time.deltaTime;

            //schooting
            if (jumpingCounter > 2)
            {
                playSchootAnimation = true;

                if (goingLeft)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (i == 0)
                        {
                            GameObject lavaBubblePrefabLeft = Instantiate(lavaBubble, transform.position + new Vector3(-3, 0, 0), Quaternion.identity);
                            lavaBubblePrefabLeft.GetComponent<BunnyFireBall>().isLeft = true;
                            Destroy(lavaBubblePrefabLeft, 2);
                        }

                        else
                        {
                            GameObject lavaBubblePrefabLeft = Instantiate(lavaBubble, transform.position + new Vector3(-1, 0, 0), Quaternion.identity);
                            lavaBubblePrefabLeft.GetComponent<BunnyFireBall>().isLeft = true;
                            Destroy(lavaBubblePrefabLeft, 2);
                        }
                    }

                    Quaternion target = Quaternion.Euler(bunnyMesh.transform.rotation.x, -90, bunnyMesh.transform.rotation.z);

                    // rotate player when turning
                    bunnyMesh.transform.localRotation = target;
                }

                if (goingRight)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (i == 0)
                        {
                            GameObject lavaBubblePrefabRight = Instantiate(lavaBubble, transform.position + new Vector3(3, 0, 0), Quaternion.identity);
                            lavaBubblePrefabRight.GetComponent<BunnyFireBall>().isRight = true;
                            Destroy(lavaBubblePrefabRight, 2);
                        }

                        else
                        {
                            GameObject lavaBubblePrefabRight = Instantiate(lavaBubble, transform.position + new Vector3(1, 0, 0), Quaternion.identity);
                            lavaBubblePrefabRight.GetComponent<BunnyFireBall>().isRight = true;
                            Destroy(lavaBubblePrefabRight, 2);
                        }
                    }

                    Quaternion target = Quaternion.Euler(bunnyMesh.transform.rotation.x, 90, bunnyMesh.transform.rotation.z);

                    // rotate player when turning
                    bunnyMesh.transform.localRotation = target;
                }
                jumpingCounter = 0;
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
        if (collision.gameObject.tag == "doesDamage")
        {
            Physics.IgnoreCollision(GetComponent<BoxCollider>(), collision.gameObject.GetComponent<Collider>());
        }
    }
}
