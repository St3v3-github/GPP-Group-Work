using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DJumpPowerUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

        if (playerInventory != null)
        {
            playerInventory.JumpCollected();
            gameObject.SetActive(false);
        }
    }
}
