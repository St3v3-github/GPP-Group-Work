using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSphere : MonoBehaviour
{
    public GameObject triggerSphere;

    void Start()
    {
        triggerSphere.SetActive(false);
    }

}
