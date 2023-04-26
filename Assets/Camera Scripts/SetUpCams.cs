using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpCams : MonoBehaviour
{
    public Camera MainCam;
   // public Camera panelTriggeredCam;
    public Camera shakeCam;
    public Camera explosionCam;
   /* public Camera DoorCam;
    public Camera MoveUpCam;
    public Camera StaticSetPCam;
    public Camera StaticShakeCam;
    */

    void Start()
    {
        MainCam.enabled = true;
        //panelTriggeredCam.enabled = false;
        shakeCam.enabled = false;
        explosionCam.enabled = false;

        /*
        DoorCam.enabled = false;
        MoveUpCam.enabled = false;
        StaticSetPCam.enabled = false;
        StaticShakeCam.enabled = false;
        */
        
    }

}
