using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPlayer : MonoBehaviour
{

    public GameObject leftLight;
    public GameObject rightLight;
    public float rotationAxis;
    public GameObject menuManager;
    public GameObject player;
    public float beginHeight;
    private void Start()
    {
        beginHeight = transform.localPosition.y;
    }

    void Update()
    {
        rotationAxis = Input.GetAxis("Horizontal");
        if(menuManager.GetComponent<CheckMenu>().menuIsActive == false && player.GetComponent<MovementPlayer>().isOnRightWall == false && player.GetComponent<MovementPlayer>().isOnLeftWall == false)
        {
            if (rotationAxis > 0)
            {
                rightLight.SetActive(true);
                leftLight.SetActive(false);
            }

            if (rotationAxis < 0)
            {
                leftLight.SetActive(true);
                rightLight.SetActive(false);
            }
        }
        
        if(player.GetComponent<MovementPlayer>().isRolling == true || player.GetComponent<MovementPlayer>().isCrouching == true)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -0.5f, transform.localPosition.z);
        }

        else
        {
            transform.localPosition = new Vector3(transform.localPosition.x, beginHeight, transform.localPosition.z);
        }
    }
}
