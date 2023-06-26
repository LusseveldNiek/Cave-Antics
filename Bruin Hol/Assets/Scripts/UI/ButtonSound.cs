using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource backSound;
    public AudioSource goSound;
    

    
    public void Back()
    {
        backSound.Play();
    }

    public void Go()
    {
        goSound.Play();
    }
}
