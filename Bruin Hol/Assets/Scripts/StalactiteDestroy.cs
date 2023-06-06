using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalactiteDestroy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
