using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ScoreSystem : MonoBehaviour
{
    public float score;
    public TMPro.TMP_Text scoreText;

    public GameObject coalImage;
    public bool coalMining;
    private float timerCoal;

    public GameObject sulfurImage;
    public bool sulfurMining;
    private float timerSulfur;

    public float diamondScore;
    public GameObject diamondImage;
    public bool diamondMining;
    private float timerDiamond;
    public GameObject[] particles;
    public Animator animator;

    public GameObject endingCanvas;
    public Button button;
    private float canvasTime;
    public bool canvasBeginTime;
    public GameObject player;

    void Update()
    {
        if (Gamepad.all[0].leftShoulder.ReadValue() > 0)
        {
            for (int i = 0; i < particles.Length; i++)
            {
                particles[i].SetActive(true);
            }
        }
        scoreText.text = "Diamonds " + score.ToString();
        if(diamondMining)
        {
            print("diamond");
            diamondImage.GetComponent<Animator>().SetBool("animation", true);
            timerDiamond += Time.deltaTime;
            if(timerDiamond > 3)
            {
                score += diamondScore;
                diamondImage.GetComponent<Animator>().SetBool("animation", false);
                print("hoi");
                diamondMining = false;
                timerDiamond = 0;

                for(int i = 0; i < particles.Length; i++)
                {
                    if(particles[i].activeInHierarchy == false)
                    {
                        particles[i].SetActive(true);
                        break;
                    }
                }
            }
        }

        if(coalMining)
        {
            print("coal");
            coalImage.GetComponent<Animator>().SetBool("animation", true);
            timerCoal += Time.deltaTime;
            if (timerCoal > 3)
            {
                coalImage.GetComponent<Animator>().SetBool("animation", false);
                print("hoi");
                coalMining = false;
                timerCoal = 0;
            }
        }

        if (sulfurMining)
        {
            print("sulfur");
            sulfurImage.GetComponent<Animator>().SetBool("animation", true);
            timerSulfur += Time.deltaTime;
            if (timerSulfur > 3)
            {
                sulfurImage.GetComponent<Animator>().SetBool("animation", false);
                print("hoi");
                sulfurMining = false;
                timerSulfur = 0;
            }
        }

        if(canvasBeginTime)
        {
            canvasTime += Time.deltaTime;
            if (canvasTime > 2)
            {
                endingCanvas.SetActive(true);
                button.Select();
                player.GetComponent<MovementPlayer>().enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            for (int i = 0; i < particles.Length; i++)
            {
                if (particles[i].activeInHierarchy == true)
                {
                    if(i == 8)
                    {
                        animator.SetBool("ending", true);
                        canvasBeginTime = true;
                        
                        
                    }
                }
            }
        }
    }
}
