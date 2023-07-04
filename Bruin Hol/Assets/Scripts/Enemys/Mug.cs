using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mug : MonoBehaviour
{
    public GameObject particle;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "pickaxe")
        {
            Destroy(gameObject);
            GameObject particlePrefab = Instantiate(particle, transform.position, Quaternion.identity);
            Destroy(particlePrefab, 1);
        }
    }
}
