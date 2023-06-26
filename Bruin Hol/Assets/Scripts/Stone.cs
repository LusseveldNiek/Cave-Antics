using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public GameObject particle;
    public GameObject soundManager;
    private void Start()
    {
        soundManager = GameObject.Find("soundManager");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "pickaxe")
        {
            soundManager.GetComponent<Sound>().isMining = true;
            Destroy(gameObject);
            GameObject particlePrefab = Instantiate(particle, transform.position, Quaternion.identity);
            Destroy(particlePrefab, 4f);
        }
    }
}
