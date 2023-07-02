using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalactiteDestroy : MonoBehaviour
{
    public GameObject particle;
    private void OnCollisionEnter(Collision collision)
    {
        GameObject particlePrefab = Instantiate(particle, collision.transform.position, Quaternion.identity);
        Destroy(particlePrefab, 0.7f);
        particlePrefab.GetComponent<AudioSource>().Play();
        Destroy(gameObject);

    }
}
