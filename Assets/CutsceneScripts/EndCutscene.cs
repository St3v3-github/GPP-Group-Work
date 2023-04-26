using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCutscene : MonoBehaviour
{
    public Camera thisCam;
    public Camera liveCam;
    ActivateSphere activateSphere;
    public GameObject sphere;
    public GameObject invisibleWall;
    public float timer = 8f;
    PlayerController playerController;

    private void Awake()
    {
        activateSphere = sphere.GetComponent<ActivateSphere>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (thisCam.enabled)
        {
            timer -= Time.deltaTime;
            activateSphere.triggerSphere.SetActive(true);

            if (timer <= 0)
            {
                thisCam.enabled = false;
                liveCam.enabled = true;
                activateSphere.triggerSphere.SetActive(false);
                invisibleWall.SetActive(false);
                playerController.speed = 2;
                playerController.speed2 = 4;
                playerController.jumpHeight = 3;
            }

        }
    }
}
