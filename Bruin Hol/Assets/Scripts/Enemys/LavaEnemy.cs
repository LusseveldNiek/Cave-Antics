using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaEnemy : MonoBehaviour
{
    public bool goingLeft;
    public bool goingRight;
    public float speed;
    public GameObject mesh;

    private void Update()
    {
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

    

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wall"))
        {
            if (goingLeft)
            {
                goingRight = true;
                goingLeft = false;
            }

            if (goingRight)
            {
                goingLeft = true;
                goingRight = false;
            }
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "doesDamage")
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }
}