using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tools : MonoBehaviour
{
    public GameObject player;
    public ScoreSystem score;
    
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            for (int i = 0; i < score.particles.Length; i++)
            {
                if (score.particles[i].activeInHierarchy == true)
                {
                    if (i == 8)
                    {
                        score.animator.SetBool("ending", true);
                        score.canvasBeginTime = true;


                    }
                }
            }
        }
    }
}
