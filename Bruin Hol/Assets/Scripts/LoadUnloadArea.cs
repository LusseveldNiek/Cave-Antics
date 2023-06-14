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
        float rightDistance = transform.position.x + 4;

        if(transform.position.x < player.transform.position.x && player.transform.position.x < rightDistance && transform.position.y - player.transform.position.y < 7)
        {
            areaToDisable.SetActive(false);
            areaToEnable.SetActive(true);
        }

        else if(player.transform.position.x < rightDistance && transform.position.y - player.transform.position.y < 7)
        {
            areaToDisable.SetActive(true);
            areaToEnable.SetActive(false);
        }
        
    }
}
