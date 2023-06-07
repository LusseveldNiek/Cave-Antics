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
    public Animator animator;

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
            pickaxeObject.SetActive(true);
            //attack
            doingDamage = true;
        }

        if (doingDamage)
        {
            animator.SetBool("isMining", true);
            attackTime += Time.deltaTime;
            if (attackTime > attackSpeed)
            {
                animator.SetBool("isMining", false);
                pickaxeObject.SetActive(false);
                attackTime = 0;
                doingDamage = false;
                //attackTime
            }
        }
    }

    void PickaxeRotation()
    {
        //turn weapon 
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
