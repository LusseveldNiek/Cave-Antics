using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    public GameObject player;
    private bool gameStarted;
    public GameObject menu;
    public Button pauseButton;

    private void Update()
    {
        gameStarted = player.GetComponent<MovementPlayer>().gameStarted;

        if (gameStarted && Gamepad.all[0].startButton.isPressed)
        {
            menu.SetActive(true);
            pauseButton.Select();
        }
    }

    
}
