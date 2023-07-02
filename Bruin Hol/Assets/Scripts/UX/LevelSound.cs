using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSound : MonoBehaviour
{
    public AudioSource desertWalk;

    public AudioSource caveWalk;

    public GameObject desertParticle;
    public GameObject sprintParticle1;
    public GameObject sprintParticle;

    public GameObject desertJumpParticle;
    public GameObject caveJumpParticle;

    public GameObject player;
    public GameObject soundManager;

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.transform.position, transform.position) < 3)
        {
            if (player.transform.position.x < transform.position.x)
            {
                player.GetComponent<MovementPlayer>().sprintParticle = sprintParticle;
                player.GetComponent<MovementPlayer>().sprintParticle1 = sprintParticle1;
                AudioSource[] audioSources = GetComponents<AudioSource>();

                foreach (AudioSource audioSource in audioSources)
                {
                    audioSource.Stop();
                }
                soundManager.GetComponent<Sound>().runningSound = caveWalk;
                player.GetComponent<MovementPlayer>().groundParticle = desertJumpParticle;
            }

            if (player.transform.position.x > transform.position.x)
            {
                player.GetComponent<MovementPlayer>().sprintParticle = desertParticle;
                player.GetComponent<MovementPlayer>().sprintParticle1 = desertParticle;

                AudioSource[] audioSources = GetComponents<AudioSource>();

                foreach (AudioSource audioSource in audioSources)
                {
                    audioSource.Stop();
                }
                soundManager.GetComponent<Sound>().runningSound = desertWalk;
                player.GetComponent<MovementPlayer>().groundParticle = desertJumpParticle;
            }
        }
        
    }
}
