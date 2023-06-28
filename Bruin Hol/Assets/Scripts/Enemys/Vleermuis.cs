using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vleermuis : MonoBehaviour
{
    public GameObject player;
    public GameObject playerMesh;
    public float distance;
    public Animator animator;
    public bool isAttacking;
    public bool goingLeft;
    public bool goingRight;
    public float speed;
    public float goingDownSpeed;
    private float targetHeight;
    public bool goingUp;
    void Start()
    {
        targetHeight = transform.position.y;
    }

    
    void Update()
    {
        if(goingLeft)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);

            Quaternion target = Quaternion.Euler(-90, -90, playerMesh.transform.rotation.z);

            playerMesh.transform.localRotation = target;
        }

        if(goingRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);

            Quaternion target = Quaternion.Euler(-90, 90, playerMesh.transform.rotation.z);

            playerMesh.transform.localRotation = target;
        }

        if (Vector3.Distance(player.transform.position, transform.position) < distance && goingUp == false)
        {
            isAttacking = true;
        }

        if(isAttacking)
        {
            animator.SetBool("gliding", true);

            if(player.transform.position.y < transform.position.y)
            {
                transform.Translate(Vector3.down * goingDownSpeed * Time.deltaTime);
            }
            
            else
            {
                print("goingDown");
                goingUp = true;
                isAttacking = false;
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
                animator.SetBool("gliding", false);
                goingUp = false;
                if (goingLeft)
                {
                    goingLeft = false;
                    goingRight = true;
                    print("turn");
                }

                else if (goingRight)
                {
                    goingLeft = true;
                    goingRight = false;
                }
            }
        }
    }
}
