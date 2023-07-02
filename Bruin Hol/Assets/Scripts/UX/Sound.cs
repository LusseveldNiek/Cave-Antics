using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public bool sprinting;
    public bool isDamaged;
    public bool isMining;
    public bool buttonPressed;
    public bool isRolling;

    public GameObject buttonType;
    public GameObject player;
    public int runningSoundDelay;
    public RaycastHit hit;

    [Header("audioSource")]
    public AudioSource theme;
    public AudioSource runningSound;
    public AudioSource miningSound;
    public AudioSource damageSound;
    public AudioSource landingSound;
    public AudioSource rollingSound;

    public AudioSource backSound;
    public AudioSource goSound;
    


    void Start()
    {
        theme.Play();
    }

    private void Update()
    {
        sprinting = player.GetComponent<MovementPlayer>().sprinting;
        isDamaged = player.GetComponent<HealthSystem>().gettingDamage;
        isRolling = player.GetComponent<MovementPlayer>().isRolling;

        //sprinting
        if (sprinting && !runningSound.isPlaying)
        {
            runningSound.time = runningSoundDelay;
            runningSound.Play();

        }
        else if (!sprinting && runningSound.isPlaying)
        {
            runningSound.Stop();
        }

        //mining
        if(isMining && !miningSound.isPlaying)
        {
            miningSound.Play();
            isMining = false;
        }

        //rolling
        if(isRolling)
        {
            rollingSound.Play();
        }

        //damage
        if(isDamaged && !damageSound.isPlaying)
        {
            damageSound.time = 1;
            damageSound.Play();
        }

        //button
        if(buttonPressed)
        {
            if(buttonType != null)
            {
                if (buttonType.tag == "buttonBack")
                {
                    backSound.Play();
                }

                if (buttonType.tag == "buttonGo")
                {
                    goSound.Play();
                }
            }

            buttonPressed = false;
        }

        //landing
        Physics.Raycast(Vector3.down, player.transform.position - new Vector3(0, 2, 0), out hit, 2);
        
        if(hit.distance > 1f && hit.distance < 1.4f)
        {
            //landingSound.Play();
            //print("landing");
        }
        
    }
}
