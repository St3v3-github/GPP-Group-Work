using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent enemyAgent;
    public Transform player;
    public LayerMask groundMask;
    public LayerMask playerMask;

    public Vector3 movePoint;
    bool movePointSet;
    public float movePointRange;

    public float timeBetweenAttacks;
    bool hasAttacked;
    public bool hasMovedBack;

    public float sightRange;
    public float attackRange;
    public bool playerInSightRange;
    public bool playerInAttackRange;

    PlayerController playerController;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAgent = GetComponent<NavMeshAgent>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);

        if(!playerInSightRange && !playerInAttackRange)
        {
            Patrolling();
            //Debug.Log("patrol");
        }
        if(playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
            //Debug.Log("chase");
        }
        if(playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
            //Debug.Log("attack");
        }
    }

    private void Patrolling()
    {
        
        if (!movePointSet)
        {
            SearchMovePoint();
        }

        if(movePointSet)
        {
            enemyAgent.SetDestination(movePoint);
        }

        Vector3 distanceToMovePoint = transform.position - movePoint;

        if(distanceToMovePoint.magnitude < 1f)
        {
            movePointSet = false;
        }
    }

    private void SearchMovePoint()
    {
        float randomZ = Random.Range(-movePointRange, movePointRange);
        float randomX = Random.Range(-movePointRange, movePointRange);

        movePoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(movePoint, -transform.up, 2f, groundMask))
        {
            movePointSet = true;
        }
    }

    private void ChasePlayer()
    {
        enemyAgent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        enemyAgent.SetDestination(transform.position);
        //transform.LookAt(player);

        if(!hasAttacked)
        {
            if (playerController.isAttacking)
            {
                Debug.Log("player attacking");
                return;
            }

            if(!playerController.isAttacking)
            {
                Debug.Log("player NOT attacking");
                //Attack code here
                playerController.TakeDamage(1);

                hasAttacked = true;
                hasMovedBack = false;

                Invoke(nameof(MoveBackwards), 1f);
                //transform.position += new Vector3(-5, 0, 0);
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }

        }
    }

    private void MoveBackwards()
    {
        Vector3 moveDirection = transform.position - player.position;
        Vector3 targetPos = transform.position + moveDirection.normalized * 5f;
        //transform.Translate(moveDirection.normalized * 7f);

        StartCoroutine(MoveToPos(targetPos, 1f));

        hasMovedBack = true;
    }

    private IEnumerator MoveToPos(Vector3 targetPos, float duration)
    {
        float elapsedTime = 0f;
        Vector3 startPos = transform.position;

        while(elapsedTime < duration)
        {
            Vector3 newPos = Vector3.Lerp(startPos, targetPos, elapsedTime / duration);
            newPos.y = transform.position.y;
            transform.position = newPos;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        hasMovedBack = false;
    }

    private void ResetAttack()
    {
        hasAttacked = false;
        
    }
}
