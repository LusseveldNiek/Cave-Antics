using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VierkantSteen : MonoBehaviour
{
    public float animationTime;
    private float animationTimer;

    void Update()
    {
        animationTimer += Time.deltaTime;
        if(animationTimer > animationTime)
        {
            transform.position -= new Vector3(8.3f, 0, 0);
            animationTimer = 0;
        }
    }

    
}
