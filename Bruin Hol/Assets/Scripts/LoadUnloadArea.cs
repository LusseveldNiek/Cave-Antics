using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadUnloadArea : MonoBehaviour
{
    public GameObject areaToDisable;
    public GameObject areaToEnable;
    public GameObject player;

    public void Update()
    {
        if(transform.position.x < player.transform.position.x)
        {
            areaToDisable.SetActive(false);
            areaToEnable.SetActive(true);
        }

        else
        {
            areaToDisable.SetActive(false);
            areaToEnable.SetActive(true);
        }
        
    }
}
