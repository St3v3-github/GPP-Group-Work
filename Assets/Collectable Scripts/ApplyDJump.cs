using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDJump : MonoBehaviour
{
    PlayerController playerController;
    public float timer = 10.0f;
    public GameObject Camera;
    public GameObject particle;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.jumpBoost == true)
        {
            playerController.canDoubleJump = true;
            particle.SetActive(true);
            timer -= Time.deltaTime;
            //Camera.layer = LayerMask.NameToLayer("PostProcess");

            if (timer <= 0.0f)
            {
                playerController.jumpBoost = false;
                //Camera.layer = LayerMask.NameToLayer("Default");
                Debug.Log("jump end ");

                timer = 10.0f;
            }
        }
        if(playerController.jumpBoost == false)
        {
            particle.SetActive(false);
            playerController.canDoubleJump = false;
        }

    }
}
