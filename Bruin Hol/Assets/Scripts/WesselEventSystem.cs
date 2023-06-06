using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WesselEventSystem : MonoBehaviour
{
    public GameObject[] areasToDisable;

    void Start()
    {
        for (int i = 0; i < areasToDisable.Length; i++)
        {
            areasToDisable[i].SetActive(false);
        }
    }
}
