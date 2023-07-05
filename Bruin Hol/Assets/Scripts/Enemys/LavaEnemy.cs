using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaEnemy : MonoBehaviour
{
    public bool goingLeft;
    public bool goingRight;
    public float speed;
    public GameObject mesh;
    public Transform player;
    public Animator animator;
    
    private void Start()
    {
        
    }

    private void Update()
    {
        if (Vector3.Distance(player.position, transform.position) < 7)
        {
            
            animator.SetFloat("speed", 1);

        }

        else if(Vector3.Distance(player.position, transform.position) < 10)
        {
            animator.SetFloat("speed", -1);
            
        }

        else
        {
            animator.SetFloat("speed", 0);
        }

        if (goingLeft)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            Quaternion target = Quaternion.Euler(mesh.gameObject.transform.rotation.x, mesh.gameObject.transform.rotation.y, 50);

            // rotate player when turning
            mesh.gameObject.transform.localRotation = target;

            
        }

        if (goingRight)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            Quaternion target = Quaternion.Euler(mesh.gameObject.transform.rotation.x, mesh.gameObject.transform.rotation.y, -50);

            // rotate player when turning
            mesh.gameObject.transform.localRotation = target;

            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "wall")
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }

        if (collision.gameObject.CompareTag("wall"))
        {
            if (goingLeft)
            {
                goingRight = true;
                goingLeft = false;
            }

            else if (goingRight)
            {
                goingLeft = true;
                goingRight = false;
            }

        }
    }
}
