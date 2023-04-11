using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp1 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

        if(playerInventory != null)
        {
            playerInventory.SpeedCollected();
            gameObject.SetActive(false);
        }
    }
}
