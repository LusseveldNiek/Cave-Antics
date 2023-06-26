using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalactiteDestroy : MonoBehaviour
{
    public GameObject particle;
    public AudioSource sound;
    private void OnCollisionEnter(Collision collision)
    {
        sound.Play();
        GameObject particlePrefab = Instantiate(particle, collision.transform.position, Quaternion.identity);
        Destroy(particlePrefab, 0.7f);
        GetComponent<MeshRenderer>().enabled = false;
        transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        Destroy(gameObject, 1);

    }
}
