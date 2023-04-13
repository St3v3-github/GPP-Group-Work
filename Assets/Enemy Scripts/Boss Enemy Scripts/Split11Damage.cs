using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split11Damage : MonoBehaviour
{
    //EnemyAI enemyAI;
    EnemyDamageController enemyDamageController;
    PlayerController playerController;
    public GameObject sword;
    public bool hasAttacked = false;
    public float delay = 1;

    [SerializeField] private Renderer myModel;
    public Material defaultMat;
    public Material redMat;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        //enemyAI = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyAI>();
        enemyDamageController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyDamageController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider Sword = sword.GetComponent<Collider>();
        if (other == Sword && playerController.isAttacking && !hasAttacked)
        {
            hasAttacked = true;
            myModel.material = redMat;
            Debug.Log("Working 11");
            Invoke(nameof(CheckAttack), delay);
        }
    }

    public void CheckAttack()
    {
        //enemyAI.TakeDamageSplit11(1);
        enemyDamageController.TakeDamageSplit11(1);
        hasAttacked = false;
        myModel.material = defaultMat;

    }
}
