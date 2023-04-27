using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBlowup : MonoBehaviour
{
    public GameObject target;
    public Material myMat;
    public ParticleSystem effect;
    bool explode = false;
    float timer = 4.5f;
    public GameObject cutSceneCam;
    public Camera maincam;
    bool cutscene = false;
    public GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        myMat.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        if (explode)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                effect.Play();
                Destroy(target);
                explode = false;
            }
        }
        else
        {
            effect.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !cutscene)
        {
            text.SetActive(false);
            cutscene = true;
            cutSceneCam.SetActive(true);
            maincam.enabled = false;
            cutSceneCam.GetComponentInChildren<Camera>().enabled = true;
            explode = true;
            myMat.color = Color.green;
        }
    }
}
