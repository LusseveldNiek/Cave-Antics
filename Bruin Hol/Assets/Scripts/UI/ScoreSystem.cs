using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public float score;
    public TMPro.TMP_Text scoreText;

    void Update()
    {
        scoreText.text = score.ToString();
    }
}
