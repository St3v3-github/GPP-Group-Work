using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI jumpText;
    public TextMeshProUGUI speedText;
    PlayerController playerController;

    ApplyDJump applyDJump;
    SpeedPowerUp speedPowerUp;

    private void Awake()
    {
        applyDJump = GameObject.FindGameObjectWithTag("Player").GetComponent<ApplyDJump>();
        speedPowerUp = GameObject.FindGameObjectWithTag("Player").GetComponent<SpeedPowerUp>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //coinText = GetComponent<TextMeshProUGUI>();
       // jumpText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateCoinText(PlayerInventory playerInventory)
    {
        coinText.text = playerInventory.NumberOfCoins.ToString();
    }

    public void UpdateJumpTimerText()
    {
        jumpText.text = applyDJump.timer.ToString("0.0");
        if(playerController.jumpBoost == false)
        {
            jumpText.SetText("0.0");
        }
    }

    public void UpdateSpeedTimerText()
    {
        speedText.text = speedPowerUp.timer.ToString("0.0");
        if(playerController.speedBoost == false)
        {
            speedText.SetText("0.0");
        }
    }

    void Update()
    {
        UpdateJumpTimerText();
        UpdateSpeedTimerText();
    }
}
