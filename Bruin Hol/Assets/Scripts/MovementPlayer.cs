using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    public float speedPlayer;
    public float jumpForce;
    public Rigidbody playerRigidbody;

    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        //horizontal movement
        transform.Translate(Input.GetAxis("Horizontal") * speedPlayer * Time.deltaTime, 0, 0);

        //jumping
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce);
        }

        if(isGrounded == false)
        {
            GetComponent<Rigidbody>().mass += Time.deltaTime * 8;
        }

        else
        {
            GetComponent<Rigidbody>().mass = 1;
        }
    }

    //is on ground
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
