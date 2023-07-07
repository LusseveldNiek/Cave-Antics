using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tools : MonoBehaviour
{
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Gamepad.all[0].leftTrigger.ReadValue() > 0)
        {
            player.transform.position = transform.position;
        }
    }
}
