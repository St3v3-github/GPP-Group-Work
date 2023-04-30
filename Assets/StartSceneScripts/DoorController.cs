using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorController : MonoBehaviour
{
    [SerializeField] Animator switchAnim;
    [SerializeField] Animator doorAnim;

    public GameObject player;
    public PlayerController playerref;

    bool playerCollide = false;
    bool opening = false;
    bool opened = false;
    float delay = .5f;
    bool press = false;

    private void Start()
    {
        playerref = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerref.interact && playerCollide)
        {
            switchAnim.SetBool("press", true);
            opening = true;
            opened = true;
            playerref.interact = false;
        }
        else if(playerref.interact)
        {
            playerref.interact = false;
        }

        if (opened)
        {
            if (opening)
            {
                if (delay > 0)
                {
                    delay -= Time.deltaTime;
                }
                else
                {
                    doorAnim.SetBool("open", true);
                    switchAnim.SetBool("press", false);
                    opening = false;
                    delay = .5f;
                }
            }
            else
            {
                if (delay > 0)
                {
                    delay -= Time.deltaTime;
                }
                else
                {
                    doorAnim.SetBool("open", false);
                    opened = false;
                }
            }
        }
    }

        private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerCollide = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerCollide = false;
        }
    }
}
