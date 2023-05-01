using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineManager : MonoBehaviour
{
    //InputManager inputManager;
    PlayerController playerMotion;
    CameraController cameraManager;

    public float interpolateAmount;

    private Vector3 cameraFollowVelocity = Vector3.zero;

    [SerializeField] public Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private Transform pointC;
    [SerializeField] private Transform pointD;

    [SerializeField] private Transform pointSplineCam;

    private Vector3 positionInSpline;
    //private GameObject splineCam;
    private GameObject MainCam;

    public bool InBox = false;


    void Awake()
    {
        //inputManager = FindObjectOfType<InputManager>();
        playerMotion = FindObjectOfType<PlayerController>();
        cameraManager = FindObjectOfType<CameraController>();

        /*        splineCam = GameObject.Find("splineCam");
                splineCam.SetActive(false);*/

        /*        MainCam = GameObject.Find("MainCam");
                MainCam.SetActive(true);*/
    }


    void Update()
    {
        //inverse lerp to get the players x position in relation to the start and finish then devide it over the finish
        //to get the percentage for the interpolateAmount.
        interpolateAmount = InverseLerp(pointA.position, pointD.position, playerMotion.transform.position);

        //Interpolates between 4 points using 2 functions, the first interpolates a-b & b-c and then interpolates between them
        // to get a-b-c then interpolates between a-b-c & b-c-d
        positionInSpline = CubicLerp(pointA.position, pointB.position, pointC.position, pointD.position, interpolateAmount);

        if (InBox)
        {
            pointSplineCam.position = positionInSpline;
            //cameraManager.
        }
    }

    //stage 1
    public static float InverseLerp(Vector3 a, Vector3 b, Vector3 value)
    {
        Vector3 AB = b - a;
        Vector3 AV = value - a;

        return Vector3.Dot(AV, AB) / Vector3.Dot(AB, AB);
    }

    //stage 2
    private Vector3 QuadraticLerp(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        Vector3 ab = Vector3.Lerp(a, b, t);
        Vector3 bc = Vector3.Lerp(b, c, t);

        return Vector3.Lerp(ab, bc, interpolateAmount);
    }

    //stage 3
    private Vector3 CubicLerp(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
    {
        Vector3 ab_bc = QuadraticLerp(a, b, c, t);
        Vector3 bc_cd = QuadraticLerp(b, c, d, t);

        return Vector3.Lerp(ab_bc, bc_cd, interpolateAmount);
    }




    //Player detection + enable/disable spline cam
    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            // splineCam.SetActive(true);
            //MainCam.SetActive(false);

            InBox = true;

        }
    }


    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            //splineCam.SetActive(false);
            //MainCam.SetActive(true);

            InBox = false;
        }
    }
}

