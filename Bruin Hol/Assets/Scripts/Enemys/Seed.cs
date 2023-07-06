using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public Animator animator;
    public float speed;
    public float maxJumpSpeed;
    private float jumpTime;
    private float maxJumpTime;

    public bool isJumping;
    public bool goingRight;
    public bool goingLeft;
    public GameObject mesh;
    public GameObject particle;

    private float walkingTime;
    private bool wallTimer;
    private float wallTime;
   

    
    void Update()
    {
        RandomManager();
        animator.SetBool("isWalking", true);
        if (goingLeft)
        {
            Quaternion target = Quaternion.Euler(-90, 90, mesh.transform.rotation.z);

            // rotate when turning
            mesh.transform.rotation = target;

            transform.Translate(Vector3.left * speed);
        }

        else if (goingRight)
        {
            Quaternion target = Quaternion.Euler(-90, -90, mesh.transform.rotation.z);

            // rotate when turning
            mesh.transform.rotation = target;


            transform.Translate(Vector3.right * speed);
        }

        if(wallTimer)
        {
            wallTime += Time.deltaTime;
            if(wallTime > 1)
            {
                wallTime = 0;
                wallTimer = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "wall" && wallTimer == false)
        {
            if(goingRight)
            {
                goingRight = false;
                goingLeft = true;
                wallTimer = true;
            }

            else if(goingLeft)
            {
                goingRight = true;
                goingLeft = false;
                wallTimer = true;
            }
        }

        if(collision.gameObject.tag == "doesDamage")
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
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
        jumpTime += Time.deltaTime;
        if (jumpTime > maxJumpTime)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * maxJumpSpeed);
            maxJumpTime = Random.Range(2, 7);
            jumpTime = 0;
        }
       
        if(wallTimer == false)
        {
            walkingTime += Time.deltaTime;
        }
        
        if(walkingTime > 1)
        {
            int random = Random.Range(0, 2);
            if(random == 0)
            {
                if(goingLeft)
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

            walkingTime = 0;
        }
    }


}
