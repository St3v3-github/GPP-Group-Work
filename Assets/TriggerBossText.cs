using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBossText : MonoBehaviour
{
    RemoveBossText removeBossText;

    private void Awake()
    {
        removeBossText = GameObject.FindGameObjectWithTag("GameController").GetComponent<RemoveBossText>();
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject PlayerCharacter = GameObject.FindGameObjectWithTag("Player");
        Collider PlayerCollider = PlayerCharacter.GetComponent<Collider>();

        if (other == PlayerCollider)
        {
            removeBossText.showBossText = true;
        }
    }
}
