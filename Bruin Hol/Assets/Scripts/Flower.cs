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
    void Update()
    {
        if(Vector3.Distance(player.transform.position, transform.position) < 10)
        {
            bulletTimer += Time.deltaTime;
            if(bulletTimer > bulletTimerLimit)
            {
                GameObject instantiatedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                instantiatedBullet.transform.LookAt(player.transform.position);
                Destroy(instantiatedBullet, bulletLifetime);
                bulletTimer = 0;
            }
        }
    }
}
