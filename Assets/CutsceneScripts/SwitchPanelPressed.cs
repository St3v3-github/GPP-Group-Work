using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPanelPressed : MonoBehaviour
{
    public GameObject switchPanel;
    public GameObject switchPanelDesPos;
    public float speed = 2f;
    public Camera liveCam;
    public Camera triggeredCam;
    public float delay = 0f;
    PlayerController playerController;
    public float newPos = 0;
    private bool pressed = false;
    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (pressed)
            return;
        float smooth = speed * Time.deltaTime;
        if(switchPanel != null)
        {
            if(switchPanel.transform.position != switchPanelDesPos.transform.position)
            {
                switchPanel.transform.position = Vector3.MoveTowards(switchPanel.transform.position, switchPanelDesPos.transform.position, smooth);
            }

            if(switchPanel.transform.position == switchPanelDesPos.transform.position)
            {
                speed = 0;
                ChangeCams();
            }
        }
    }

    public void ChangeCams()
    {
        delay += Time.deltaTime;

        if (delay >= 0.5)
        {
            pressed = true;
            triggeredCam.enabled = true;
            liveCam.enabled = false;
            playerController.speed = 0;
            playerController.speed2 = 0;
            playerController.jumpHeight = 0;
        }
    }

}
