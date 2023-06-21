using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{

    public AudioSource theme;

    void Start()
    {
        theme.Play();
    }
}
