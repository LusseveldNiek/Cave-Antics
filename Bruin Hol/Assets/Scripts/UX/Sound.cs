using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public bool sprinting;
    public bool isDamaged;
    public bool isMining;

    public GameObject player;
    public int runningSoundDelay;

    [Header("audioSource")]
    public AudioSource theme;
    public AudioSource runningSound;
    public AudioSource miningSound;
    public AudioSource damageSound;


    void Start()
    {
        theme.Play();
    }

    private void Update()
    {
        sprinting = player.GetComponent<MovementPlayer>().sprinting;
        isDamaged = player.GetComponent<HealthSystem>().gettingDamage;

        if (sprinting && !runningSound.isPlaying)
        {
            runningSound.time = runningSoundDelay;
            runningSound.Play();

        }
        else if (!sprinting && runningSound.isPlaying)
        {
            runningSound.Stop();
        }

        if(isMining && !miningSound.isPlaying)
        {
            miningSound.Play();
            isMining = false;
        }

        if(isDamaged && !damageSound.isPlaying)
        {
            damageSound.Play();
        }

        
    }
}
