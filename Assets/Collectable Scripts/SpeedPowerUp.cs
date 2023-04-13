using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    //SpeedCollectable speedCollectable;
    PlayerController playerController;
    //public GameObject collectable;

    public float timer = 10.0f;
    public GameObject Camera;



    private void Awake()
    {
        //collectable = GameObject.FindGameObjectWithTag("Collectable");
        //speedCollectable = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpeedCollectable>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
        //int LayerWater = LayerMask.NameToLayer("Water");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(speedCollectable.collected == true)
        if(playerController.speedBoost == true)
        {
            timer -= Time.deltaTime;
            Camera.layer = LayerMask.NameToLayer("PostProcess");
            playerController.speed = 4;
            playerController.speed2 = 6;
            //speedCollectable.collectable.SetActive(false);
            //Destroy(speedCollectable.collectable);

            if (timer <= 0.0f)
            {
                playerController.speedBoost = false;
                //speedCollectable.collected = false;
                Camera.layer = LayerMask.NameToLayer("Default");
                Debug.Log("speed end ");
                playerController.speed = 2;
                playerController.speed2 = 4;

                timer = 10.0f;
            }
        }
    }
}
