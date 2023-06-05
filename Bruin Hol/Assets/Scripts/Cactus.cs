using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    public GameObject player;
    public Animator animator;
    public Transform cactusUp;
    public bool cactusActivated;
    private float beginHeight;
    public float upSpeed;
    public float upHeight;
    public float moveSpeed;
    public bool cactusIsWalking;
    public bool cactusGoingDown;
    public float cactusGoingUpDistance;
    public float groundOffset;
    public bool hittingWall;
    void Start()
    {
        beginHeight = transform.position.y;
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit) && cactusIsWalking)
        {
            // Get the height of the ground at the hit point
            float groundHeight = hit.point.y + groundOffset;

            // Adjust the position of the object to stay on the ground
            transform.position = new Vector3(transform.position.x, groundHeight, transform.position.z);
        }

        if (Vector3.Distance(cactusUp.position, player.transform.position) < cactusGoingUpDistance && cactusGoingDown == false && cactusIsWalking == false)
        {
            cactusActivated = true;
        }

        if(cactusActivated)
        {
            if(transform.position.y < beginHeight + upHeight)
            {
                transform.Translate(Vector3.up * upSpeed * Time.deltaTime);
                animator.SetBool("isGoingUp", true);
            }

            else if(transform.position.y > beginHeight + upHeight)
            {
                
                cactusActivated = false;
                cactusIsWalking = true;
                animator.SetBool("isGoingUp", false);

            }
        }

        if(cactusGoingDown)
        {
            if (transform.position.y > beginHeight)
            {
                transform.Translate(-Vector3.up * upSpeed * Time.deltaTime);
                //going down
            }

            else if (transform.position.y < beginHeight)
            {
                cactusGoingDown = false;
                //cactus in ground again
            }
        }

        if(cactusIsWalking)
        {
            if (Vector3.Distance(cactusUp.position, player.transform.position) > cactusGoingUpDistance)
            {
                cactusGoingDown = true;
                cactusIsWalking = false;
                //player out of sight while walking
                beginHeight = transform.position.y - 2.4f;
            }

            else if(cactusGoingDown == false && hittingWall == false)
            {
                //walking
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            }

            else if(hittingWall)
            {
                transform.Translate(-Vector3.right * moveSpeed * Time.deltaTime);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "pickaxe" && cactusIsWalking)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hittingWall)
        {
            if (collision.gameObject.tag != "Player" && cactusIsWalking && collision.gameObject.tag != "Ground")
            {
                hittingWall = false;
            }
        }

        else if (collision.gameObject.tag != "Player" && cactusIsWalking && collision.gameObject.tag != "Ground")
        {
            hittingWall = true;
            print("working");
        }
    }
}
