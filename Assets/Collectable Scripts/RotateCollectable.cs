using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCollectable : MonoBehaviour
{
    void Update()
    {
        transform.localRotation = Quaternion.Euler(90.0f, Time.time * 100.0f, 0);
    }
}
