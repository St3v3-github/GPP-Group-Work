using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpEnabled : MonoBehaviour
{
    public bool jumpCollected;
    public GameObject jumpCollectable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        Collider PlayerCollider = Player.GetComponent<Collider>();

        if (other == PlayerCollider)
        {
            Debug.Log("collided jump powerup");
            jumpCollected = true;
            jumpCollectable.SetActive(false);
        }
    }
}
