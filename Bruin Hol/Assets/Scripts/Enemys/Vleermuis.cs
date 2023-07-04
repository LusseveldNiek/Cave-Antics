using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vleermuis : MonoBehaviour
{
    public GameObject player;
    public GameObject mesh;
    public float distance;
    public Animator animator;
    public bool isAttacking;
    public bool goingLeft;
    public bool goingRight;
    public float speed;
    public float goingDownSpeed;
    private float targetHeight;
    public bool goingUp;
    private RaycastHit leftHit;
    private RaycastHit rightHit;
    private RaycastHit downHit;
    public bool isColliding;
    private float collisionReset;
    void Start()
    {
        targetHeight = transform.position.y;
    }

    
    void Update()
    {
        if(goingLeft)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);

            Quaternion target = Quaternion.Euler(-90, -90, mesh.transform.rotation.z);

            mesh.transform.localRotation = target;
            Physics.Raycast(transform.position + new Vector3(-1, 0, 0), Vector3.left, out leftHit, 1);
            if(leftHit.transform != null)
            {
                if(leftHit.transform.gameObject.tag == "wall")
                {
                    goingLeft = false;
                    goingRight = true;
                }
            }
        }

        if(goingRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);

            Quaternion target = Quaternion.Euler(-90, 90, mesh.transform.rotation.z);

            mesh.transform.localRotation = target;
            Physics.Raycast(transform.position + new Vector3(1, 0, 0), Vector3.right, out rightHit, 1);
            if (rightHit.transform != null)
            {
                if (rightHit.transform.gameObject.tag == "wall")
                {
                    goingLeft = true;
                    goingRight = false;
                }
            }
        }

        if (Vector3.Distance(player.transform.position, transform.position) < distance && goingUp == false)
        {
            isAttacking = true;
        }

        if(isAttacking)
        {
            animator.SetBool("gliding", true);
            Physics.Raycast(transform.position, Vector3.down, out downHit, 0.4f);
            if(player.transform.position.y < transform.position.y && downHit.transform == null && isColliding == false)
            {
                transform.Translate(Vector3.down * goingDownSpeed * Time.deltaTime);
            }
            
            else
            {
                downHit = new RaycastHit();
                print("goingDown");
                goingUp = true;
                isAttacking = false;
                animator.SetBool("gliding", false);
            }

        }

        if(goingUp)
        {
            print("goingUp");
            if(transform.position.y < targetHeight)
            {
                transform.Translate(Vector3.up * goingDownSpeed * Time.deltaTime);
            }

            else if(Vector3.Distance(player.transform.position, transform.position) > distance)
            {
                goingUp = false;
                if (goingLeft)
                {
                    if(player.transform.position.x > transform.position.x)
                    {
                        goingLeft = false;
                        goingRight = true;
                        print("turn");
                    }
                    
                }

                else if (goingRight)
                {
                    if(player.transform.position.x < transform.position.x)
                    {
                        goingLeft = true;
                        goingRight = false;
                    }
                    
                }
            }
        }

        //reset collision timer
        if(isColliding)
        {
            collisionReset += Time.deltaTime;
            if(collisionReset > 0.2f)
            {
                isColliding = false;
                collisionReset = 0;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "pickaxe")
        {
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Player")
        {
            isColliding = true;
        }
    }
}
