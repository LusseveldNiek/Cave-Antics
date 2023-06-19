using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VierkantSteen : MonoBehaviour
{
    public GameObject cube;
    public Transform rotations;

    public Transform downRight;
    public Transform downLeft;
    public Transform upRight;
    public Transform upLeft;

    void Update()
    {
        if(cube.transform.rotation.z == 0)
        {
            //downright
            downLeft.SetParent(rotations);
            downLeft.rotation = Quaternion.identity;

            cube.transform.SetParent(downRight);
            downRight.Rotate(0, 0, 90); 
        }

        if(cube.transform.rotation.z == 90)
        {
            //upright
            downRight.SetParent(rotations);
            downRight.rotation = Quaternion.identity;

            cube.transform.SetParent(upRight);
            upRight.Rotate(0, 0, 90);
            
        }

        if (cube.transform.rotation.z == 180)
        {
            //upleft
            upRight.SetParent(rotations);
            upRight.rotation = Quaternion.identity;

            cube.transform.SetParent(upLeft);
            upLeft.Rotate(0, 0, 90);
            
        }

        if (cube.transform.rotation.z == 270)
        {
            //downleft
            upLeft.SetParent(rotations);
            upLeft.rotation = Quaternion.identity;

            cube.transform.SetParent(downLeft);
            downLeft.Rotate(0, 0, 90);
        }

    }

    
}
