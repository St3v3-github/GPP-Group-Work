using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyDamage : MonoBehaviour
{
    public float health = 2;

    PlayerController playerController;
    public GameObject sword;
    public GameObject basicEnemy;
    public GameObject basicEnemyParticles;
    public bool hasAttacked = false;
    public float delay = 1;

    [SerializeField] private Renderer myModel;
    public Material defaultMat;
    public Material redMat;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            basicEnemyParticles.transform.SetParent(null);
            basicEnemyParticles.SetActive(true);

            Debug.Log("dead");
            basicEnemy.SetActive(false);

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Collider Sword = sword.GetComponent<Collider>();
        if (other == Sword && playerController.isAttacking && !hasAttacked)
        {
            hasAttacked = true;
            myModel.material = redMat;
            Debug.Log("Working basic");
            Invoke(nameof(CheckAttack), delay);
        }
    }
    public void CheckAttack()
    {
        TakeDamage(1);
        hasAttacked = false;
        myModel.material = defaultMat;

    }
}
