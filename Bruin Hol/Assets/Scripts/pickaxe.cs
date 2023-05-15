using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickaxe : MonoBehaviour
{
    private float attackTime;
    public float attackSpeed;
    private bool doingDamage;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            GetComponent<SphereCollider>().enabled = true;
            //attack
            doingDamage = true;
        }

        if(doingDamage)
        {
            attackTime += Time.deltaTime;
            if (attackTime > attackSpeed)
            {
                GetComponent<SphereCollider>().enabled = false;
                attackTime = 0;
                doingDamage = false;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "stone")
        {
            Destroy(other.gameObject);
        }
    }
}
