using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] Animator switchAnim;
    [SerializeField] Animator doorAnim;

    bool playerCollide = false;
    bool opening = false;
    bool opened = false;
    float delay = .5f;

    // Update is called once per frame
    void Update()
    {
        if (playerCollide)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                switchAnim.SetBool("press", true);
                opening = true;
                opened = true;
            }
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
