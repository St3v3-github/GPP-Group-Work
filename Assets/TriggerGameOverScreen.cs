using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGameOverScreen : MonoBehaviour
{
    PlayerInventory playerInventory;
    PlayerController playerController;
    public GameObject gameOver;
    private void Awake()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInventory.NumberOfCoins == 15)
        {
            gameOver.SetActive(true);
            playerController.speed = 0;
            playerController.speed2 = 0;
            playerController.jumpHeight = 0;
        }
    }
}
