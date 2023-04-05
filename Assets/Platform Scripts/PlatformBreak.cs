using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBreak : MonoBehaviour
{
    public GameObject platform;
    public float destroySpeed;
    [SerializeField] private Renderer myModel;
    public Material redMaterial;
    private float alphaValue;

    // Start is called before the first frame update
    void Start()
    {
        alphaValue = myModel.material.color.a;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {

        if (platform != null)
        {
            myModel.material = redMaterial;

            Fade();
        }
    }

    public void Fade()
    {
        Color color = redMaterial.color;
        color.a = alphaValue;
        redMaterial.color = color;
        alphaValue -= destroySpeed * Time.deltaTime;
        if (alphaValue <= 0)
        {
            destroySpeed = 0;
            platform.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        Destroy(myModel.material);
    }
}
