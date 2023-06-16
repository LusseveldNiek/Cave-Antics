using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public float score;
    public TMPro.TMP_Text scoreText;
    public bool diamondMining;
    public float diamondScore;
    public GameObject diamondImage;
    private float timer;

    void Update()
    {
        scoreText.text = score.ToString();
        if(diamondMining)
        {
            print("diamond");
            diamondImage.GetComponent<Animator>().SetBool("animation", true);
            timer += Time.deltaTime;
            if(timer > 1)
            {
                score += diamondScore;
                diamondImage.GetComponent<Animator>().SetBool("animation", false);
                print("hoi");
                diamondMining = false;
                timer = 0;
            }
        }
    }
}
