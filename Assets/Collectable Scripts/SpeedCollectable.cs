using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCollectable : MonoBehaviour
{
    public bool collected;
    public bool timerStart;
    PlayerController playerController;
    //public GameObject[] collectable;
    public GameObject player;
    Collider collectableCollider;
   // public float timer = 0.0f;

    private void Awake()
    {
        //collectable = GameObject.FindGameObjectsWithTag("Collectable");
        //collectableCollider = collectable.
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(collected == false)
        {
            //Debug.Log("false");
        }
    }

   // public void OnTriggerEnter(Collider other)
   // {
       // GameObject Player = GameObject.FindGameObjectWithTag("Player");
       // Collider PlayerCollider = Player.GetComponent<Collider>();

      //  for(int i = 0; i < other.Length; i++)
       // {
        //    if(other[i] == PlayerCollider)
        //    {
        //        Debug.Log("collided");
         //       collected = true;
        //    }
       // }

   // }
}
