using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tools : MonoBehaviour
{
    public GameObject player;
    public GameObject score;
    
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
            if (score.GetComponent<ScoreSystem>().enabledDiamonds == 8)
            {
                score.GetComponent<ScoreSystem>().animator.SetBool("ending", true);
                score.GetComponent<ScoreSystem>().canvasBeginTime = true;
            }
            //for (int i = 0; i < score.GetComponent<ScoreSystem>().particles.Length; i++)
            //{
            //    if (score.GetComponent<ScoreSystem>().particles[i].activeInHierarchy == true)
            //    {
            //        if (i == 8)
            //        {


            //        }
            //    }
            //}
        }
    }
}
