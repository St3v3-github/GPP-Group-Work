using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMovePlatform : MonoBehaviour
{
    public GameObject startPos;
    public GameObject endPos;

    public float delay;
    public float speed = 4.0f;
    private bool pos = true;
    private float elaspsedTime = 0;

    private void FixedUpdate()
    {
        elaspsedTime += Time.deltaTime;

        float smooth = speed * Time.deltaTime;

        if(elaspsedTime > delay)
        {
            if(pos)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPos.transform.position, smooth);
            }

            if(!pos)
            {
                transform.position = Vector3.MoveTowards(transform.position, startPos.transform.position, smooth);
            }
        }

        if(pos)
        {
            if(transform.position == endPos.transform.position)
            {
                pos = false;
                elaspsedTime = 0;
            }
        }

        if(!pos)
        {
            if(transform.position == startPos.transform.position)
            {
                pos = true;
                elaspsedTime = 0;
            }
        }
    }
}
