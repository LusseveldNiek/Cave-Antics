using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public Animator animator;
    public float speed;
    public float jumpSpeed;
    private float idleTime;
    private float maxIdleTime;
    private float walkTime;
    private float maxWalkTime;
    public bool isWalking;
    public bool isIdle;
    public bool goingRight;
    public bool goingLeft;
    public float lowestSpeed;
    public float highestSpeed;
    private float jumpTime;
    public GameObject mesh;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    void Update()
    {
        
        

        RandomManager();
        if(isWalking)
        {

            animator.SetBool("isWalking", true);
            if(goingLeft)
            {
                Quaternion target = Quaternion.Euler(-90, -90, mesh.transform.rotation.z);

                // rotate when turning
                mesh.transform.rotation = target;

                transform.Translate(Vector3.left * speed);
            }

            else if(goingRight)
            {
                Quaternion target = Quaternion.Euler(-90, 90, mesh.transform.rotation.z);

                // rotate when turning
                mesh.transform.rotation = target;

                
                transform.Translate(Vector3.right * speed);
            }
        }

        if(isIdle)
        {
            animator.SetBool("isWalking", false);
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

        if(collision.gameObject.tag == "pickaxe")
        {
            Destroy(gameObject);
        }
    }

    void RandomManager()
    {
        jumpTime += Time.deltaTime;
        if(4 < jumpTime)
        {
            jumpTime = 0;
            jumpSpeed = Random.Range(10, 70);
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpSpeed);
        }
        

        if (isWalking)
        {
            maxIdleTime = Random.Range(2, 7);
            walkTime += Time.deltaTime;
            if(walkTime > maxWalkTime)
            {
                isIdle = true;
                isWalking = false;
                walkTime = 0;
            }

        }

        if(isIdle)
        {
            
            speed = Random.Range(lowestSpeed, highestSpeed);
            maxWalkTime = Random.Range(2, 7);
            idleTime += Time.deltaTime;
            if (idleTime > maxIdleTime)
            {
                idleTime = 0;
                isIdle = false;
                isWalking = true;
                if(goingLeft)
                {
                    goingLeft = false;
                    goingRight = true;
                }

                else if(goingRight)
                {
                    goingLeft = true;
                    goingRight = false;
                }
            }
        }
    }


}
