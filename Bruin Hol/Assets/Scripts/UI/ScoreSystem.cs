using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    void Update()
    {
        scoreText.text = score.ToString();
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
    }
}
