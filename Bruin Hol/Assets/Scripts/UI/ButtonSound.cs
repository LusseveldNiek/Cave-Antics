using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public GameObject soundManager;

    
    public void PlaySoundButton()
    {
        soundManager.GetComponent<Sound>().buttonType = transform.gameObject;
        soundManager.GetComponent<Sound>().buttonPressed = true;
    }

    
}
