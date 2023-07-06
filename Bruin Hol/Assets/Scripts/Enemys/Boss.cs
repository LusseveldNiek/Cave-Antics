using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Animator animator;
    public GameObject particle;
    private bool attack1;
    private bool attack2;
    private bool reset;
    private float resetTime;
    public float fireSpeed;

    public GameObject prefabAttack1;
    public GameObject prefabAttack2;
    
    // Start is called before the first frame update
    void Start()
    {
        reset = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(reset)
        {
            resetTime += Time.deltaTime;
            if(resetTime > 5)
            {
                attack1 = true;
                resetTime = 0;
            }
        }

        if(attack1)
        {
            animator.SetBool("attack1", true);
            attack1 = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "pickaxe")
        {
            Destroy(gameObject);
            GameObject particlePrefab = Instantiate(particle, transform.position, Quaternion.identity);
            Destroy(particlePrefab, 1);
        }
    }

    public void Schoot()
    {
        print("schootingWorks");
        animator.SetBool("attack1", false);
        GameObject prefab = Instantiate(prefabAttack1, transform.position, Quaternion.identity);
        Destroy(prefab, 4);
    }
}
