using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public GameObject mainMenu;
    private float timer;
    private bool startTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayAnimation()
    {
        // Trigger the animation transition
        mainMenu.GetComponent<Animator>().enabled = true;
        mainMenu.GetComponent<Animator>().SetTrigger("PlayAnimation");
        startTimer = true;
    }

    private void Update()
    {
        if(startTimer)
        {
            timer += Time.deltaTime;
            if(timer > 1)
            {
                mainMenu.SetActive(false);
                startTimer = false;
            }
        }
    }
}
