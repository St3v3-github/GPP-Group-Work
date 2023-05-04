using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBossText : MonoBehaviour
{
    public float timer = 5f;
    public bool showBossText = false;
    public GameObject bossText1;
    public GameObject bossText2;
    public GameObject coinImage2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(showBossText)
        {
            bossText1.SetActive(true);
            bossText2.SetActive(true);
            coinImage2.SetActive(true);
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                bossText1.SetActive(false);
                bossText2.SetActive(false);
                coinImage2.SetActive(false);
                showBossText = false;
            }
        }

    }
}
