using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBlowup : MonoBehaviour
{
    public GameObject target;
    public Material myMat;
    public ParticleSystem effect;
    bool explode = false;
    float timer = 3f;

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
        if (other.gameObject.tag == "Player")
        {
            explode = true;
            myMat.color = Color.green;
        }
    }
}
