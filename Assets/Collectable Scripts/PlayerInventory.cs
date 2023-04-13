using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfCoins { get; private set; }

    public UnityEvent<PlayerInventory> OnSpeedCollected;

    public UnityEvent<PlayerInventory> OnJumpCollected;

    public UnityEvent<PlayerInventory> OnCoinCollected;

    public void SpeedCollected()
    {
        Debug.Log("Got speed");
        OnSpeedCollected.Invoke(this);
    }

    public void JumpCollected()
    {
        Debug.Log("Got Jump");
        OnJumpCollected.Invoke(this);
    }

    public void CoinCollected()
    {
        NumberOfCoins++;
        OnCoinCollected.Invoke(this);
    }

}
