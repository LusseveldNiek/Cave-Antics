using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPlayer : MonoBehaviour
{

    public GameObject leftLight;
    public GameObject rightLight;
    public float rotationAxis;
    public GameObject menuManager;

    void Update()
    {
        rotationAxis = Input.GetAxis("Horizontal");
        if(menuManager.GetComponent<CheckMenu>().menuIsActive == false)
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
        
    }
}
