using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    public Transform player;
    public Transform respawnPoint;

    public void OnTriggerEnter(Collider other)
    {
        player.transform.position = respawnPoint.transform.position;
    }
}
