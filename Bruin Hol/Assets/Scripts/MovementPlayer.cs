using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    public float speedPlayer;
    public Rigidbody player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player.AddForce(Input.GetAxis("Horizontal") * speedPlayer, 0, 0);
        player.AddForce(0, Input.GetAxis("Vertical") * speedPlayer, 0);
    }
}
