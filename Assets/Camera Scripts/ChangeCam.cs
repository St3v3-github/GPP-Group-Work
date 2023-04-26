using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCam : MonoBehaviour
{
    public Camera thisCam;
    public Camera nextCam;
    public float timer = 5f;

    void Start()
    {

    }

    void Update()
    {
        if (thisCam.enabled == true)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                thisCam.enabled = false;
                nextCam.enabled = true;
            }
        }
    }
}
