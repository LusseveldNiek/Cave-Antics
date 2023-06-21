using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VierkantSteen : MonoBehaviour
{
    public bool goingLeft;
    public bool goingRight;

    public Transform raycastUp;
    public Transform raycastDown;
    public Transform raycastRight;
    public Transform raycastLeft;

    private RaycastHit hit;
    public Animator animator;

    void Update()
    {
        if(goingLeft)
        {
            
            Physics.Raycast(raycastUp.position, Vector3.left, out hit, 0.1f);
            Physics.Raycast(raycastDown.position, Vector3.left, out hit, 0.1f);
            Physics.Raycast(raycastLeft.position, Vector3.left, out hit, 0.1f);
            Physics.Raycast(raycastRight.position, Vector3.left, out hit, 0.1f);

            if(hit.transform != null)
            {
                if (hit.transform.gameObject.tag == "wallLeft")
                {
                    animator.SetFloat("speed", -1);
                    goingRight = true;
                    goingLeft = false;
                    hit = new RaycastHit();
                    print("hitWall");
                }
            }
            
        }

        if(goingRight)
        {
            
            Physics.Raycast(raycastUp.position, Vector3.right, out hit, 0.1f);
            Physics.Raycast(raycastDown.position, Vector3.right, out hit, 0.1f);
            Physics.Raycast(raycastLeft.position, Vector3.right, out hit, 0.1f);
            Physics.Raycast(raycastRight.position, Vector3.right, out hit, 0.1f);

            if(hit.transform != null)
            {
                if (hit.transform.gameObject.tag == "wallRight")
                {
                    animator.SetFloat("speed", 1);
                    goingRight = false;
                    goingLeft = true;
                    hit = new RaycastHit();
                }
            }
            
        }
        
    }

    public void Leftreset()
    {
        if(goingLeft)
        {
            transform.position += new Vector3(-9.288f, 0, 0);
            print("teleportingLeft");
        }
    }

    public void Rightreset()
    {
        if (goingRight)
        {
            transform.position += new Vector3(9.288f, 0, 0);
            print("teleportingRight");
        }
    }

    

}
