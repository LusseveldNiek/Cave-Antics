using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class pickaxe : MonoBehaviour
{
    private float attackTime;
    public float attackSpeed;
    private bool doingDamage;
    public GameObject pickaxeLeftObject;
    public GameObject pickaxeRightObject;
    private GameObject pickaxeObject;

    public float rotationAxis;
    // Update is called once per frame
    void Update()
    {
        Damage();
        PickaxeRotation();

    }

    void Damage()
    {
        if (Gamepad.all[0].buttonWest.isPressed && pickaxeObject != null)
        {
            pickaxeObject.GetComponent<SphereCollider>().enabled = true;
            pickaxeObject.GetComponent<MeshRenderer>().enabled = true; 
            //attack
            doingDamage = true;
        }

        if (doingDamage)
        {
            attackTime += Time.deltaTime;
            if (attackTime > attackSpeed)
            {
                pickaxeObject.GetComponent<MeshRenderer>().enabled = false;
                pickaxeObject.GetComponent<MeshRenderer>().enabled = false;
                attackTime = 0;
                doingDamage = false;
            }
        }
    }

    void PickaxeRotation()
    {
        rotationAxis = Input.GetAxis("Horizontal");
        if(rotationAxis > 0 && doingDamage == false)
        {
            pickaxeObject = pickaxeRightObject;
        }

        if(rotationAxis < 0 && doingDamage == false)
        {
            pickaxeObject = pickaxeLeftObject;
        }
    }
}
