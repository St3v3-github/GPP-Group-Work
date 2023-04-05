using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStayOnPlatform : MonoBehaviour
{
    public GameObject platform;
    private void Awake()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (platform != null)
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (platform != null)
        {
            other.transform.SetParent(null);
        }
    }
}
