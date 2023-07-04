using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public GameObject mainMenu;
    private float timer;
    public bool startTimer;
    public Button button;

    private void Start()
    {
        if(button != null)
        {
            button.Select();
        }
        
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
