using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split31Damage : MonoBehaviour
{
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
        enemyDamageController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyDamageController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider Sword = sword.GetComponent<Collider>();
        if (other == Sword && playerController.isAttacking && !hasAttacked)
        {
            hasAttacked = true;
            myModel.material = redMat;
            Debug.Log("Working 31");
            Invoke(nameof(CheckAttack), delay);
        }
    }

    public void CheckAttack()
    {
        enemyDamageController.TakeDamageSplit31(1);
        hasAttacked = false;
        myModel.material = defaultMat;

    }
}
