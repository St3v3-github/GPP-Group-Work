using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformInputs : MonoBehaviour
{
    public GameObject platform;

    public float minAmountX;
    public float maxAmountX;
    public float moveX;
    public bool switchX;

    public float minAmountY;
    public float maxAmountY;
    public float moveY;
    public bool switchY;

    public float minAmountZ;
    public float maxAmountZ;
    public float moveZ;
    public bool switchZ;

    public float speedX;
    public float speedY;
    public float speedZ;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlatformXMovement();
        PlatformYMovement();
        PlatformZMovement();
    }

    public void PlatformXMovement()
    {
        if (switchX)
        {
            platform.transform.position += new Vector3(moveX, 0, 0) * speedX * Time.deltaTime;
            if (platform.transform.position.x >= maxAmountX)
            {
                switchX = false;
            }
        }
        if (!switchX)
        {
            //moveX = -moveX;
            platform.transform.position += new Vector3(-moveX, 0, 0) * speedX * Time.deltaTime;

            if (platform.transform.position.x <= minAmountX)
            {
                switchX = true;
            }
        }
    }

    public void PlatformYMovement()
    {
        if (switchY)
        {
            platform.transform.position += new Vector3(0, moveY, 0) * speedY * Time.deltaTime;
            if (platform.transform.position.y >= maxAmountY)
            {
                switchY = false;
            }
        }
        if (!switchY)
        {
            platform.transform.position += new Vector3(0, -moveY, 0) * speedY * Time.deltaTime;

            if (platform.transform.position.y <= minAmountY)
            {
                switchY = true;
            }
        }
    }

    public void PlatformZMovement()
    {
        if (switchZ)
        {
            platform.transform.position += new Vector3(0, 0, moveZ) * speedZ * Time.deltaTime;
            if (platform.transform.position.z >= maxAmountZ)
            {
                switchZ = false;
            }
        }
        if (!switchZ)
        {
            platform.transform.position += new Vector3(0, 0, -moveZ) * speedZ * Time.deltaTime;

            if (platform.transform.position.z <= minAmountZ)
            {
                switchZ = true;
            }
        }
    }
}
