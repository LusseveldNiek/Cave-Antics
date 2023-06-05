using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public GameObject player;
    public GameObject bullet;
    private float bulletTimer;
    public float bulletTimerLimit;
    public int bulletLifetime;
    public float flowerRange;
    public Transform spawnPosition;
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < flowerRange)
        {
            bulletTimer += Time.deltaTime;
            if (bulletTimer > bulletTimerLimit)
            {
                GameObject instantiatedBullet = Instantiate(bullet, spawnPosition.position, Quaternion.identity);
                Vector3 direction = ((player.transform.position - transform.position) - new Vector3(0, 1, 0)).normalized;
                FlowerBullet bulletScript = instantiatedBullet.GetComponent<FlowerBullet>();
                bulletScript.ShootInDirection(direction);
                Destroy(instantiatedBullet, bulletLifetime);
                bulletTimer = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "pickaxe")
        {
            Destroy(gameObject);
        }
    }
}
