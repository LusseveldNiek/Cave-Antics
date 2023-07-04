using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public Animator animator;
    public float speed;
    private float maxJumpSpeed;
    private float walkTime;
    private float maxWalkTime;
    public bool isWalking;
    public bool isJumping;
    public bool goingRight;
    public bool goingLeft;
    public float lowestSpeed;
    public float highestSpeed;
    public GameObject mesh;
    public GameObject particle;
   

    
    void Update()
    {
        RandomManager();
        if(isWalking)
        {

            animator.SetBool("isWalking", true);
            if(goingLeft)
            {
                Quaternion target = Quaternion.Euler(-90, 90, mesh.transform.rotation.z);

                // rotate when turning
                mesh.transform.rotation = target;

                transform.Translate(Vector3.left * speed);
            }

            else if(goingRight)
            {
                Quaternion target = Quaternion.Euler(-90, -90, mesh.transform.rotation.z);

                // rotate when turning
                mesh.transform.rotation = target;

                
                transform.Translate(Vector3.right * speed);
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "wall")
        {
            if(goingRight)
            {
                goingRight = false;
                goingLeft = true;
            }

            else if(goingLeft)
            {
                goingRight = true;
                goingLeft = false;
            }
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "pickaxe")
        {
            Destroy(gameObject);
            GameObject particlePrefab = Instantiate(particle, transform.position, Quaternion.identity);
            Destroy(particlePrefab, 1);
        }
    }

    void RandomManager()
    {
        if (isWalking)
        {
            maxJumpSpeed = Random.Range(10, 70);
            walkTime += Time.deltaTime;
            if(walkTime > maxWalkTime)
            {
                isJumping = true;
                isWalking = false;
                walkTime = 0;
            }

        }

        if(isJumping)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * maxJumpSpeed);
            speed = Random.Range(lowestSpeed, highestSpeed);
            maxWalkTime = Random.Range(2, 7);
            
            isJumping = false;
            isWalking = true;
            if (goingLeft)
            {
                goingLeft = false;
                goingRight = true;
            }

            else if (goingRight)
            {
                goingLeft = true;
                goingRight = false;
            }
            
        }
    }


}
