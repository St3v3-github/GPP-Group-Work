using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordStay : MonoBehaviour
{
    public GameObject player;
    PlayerController playerController;
    public GameObject sword;
    public GameObject swordParent;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (sword.transform.parent != swordParent.transform)
        {
            sword.transform.SetParent(swordParent.transform);
        }

        /*
        if (playerController.isDead)
        {
            return;
        }
        else if (player.activeInHierarchy == false)
        {
            player.transform.SetParent(null);
            player.SetActive(true);
        }
        */
    }
}
