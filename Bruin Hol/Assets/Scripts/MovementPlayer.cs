using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    public float speedPlayer, jumpForce, fallSpeed;
    public Rigidbody playerRigidbody;

    public bool isGrounded;
    private RaycastHit groundHit;
    public float height;
    public float sprintSpeed;
    private float sprintCounter;
    private float beginPlayerSpeed;
    void Start()
    {
        beginPlayerSpeed = speedPlayer;
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
        
        //maakt springen korter
        IncreaseMass();
        Sprinting();
    }

    void IncreaseMass()
    {
        height = groundHit.distance;
        if (isGrounded == false)
        {
            Physics.Raycast(transform.position, -transform.up, out groundHit, 100);          
            Physics.gravity = new Vector3(0, -groundHit.distance * fallSpeed, 0);

        }

        else
        {
            Physics.gravity = new Vector3(0, -10, 0);
        }
    }

    void Sprinting()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            speedPlayer = sprintCounter * sprintCounter;
            sprintCounter += sprintSpeed * Time.deltaTime;
        }

        else
        {
            speedPlayer = beginPlayerSpeed;
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
