using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPrefab : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "doesDamage")
        {
            Destroy(gameObject);
        }
        
    }
}
