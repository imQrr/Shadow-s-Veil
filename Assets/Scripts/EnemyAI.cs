using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Processors;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health = 30.0f;
    public float DamagePerSecond = 10.0f;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange = 5f;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    // Animator
    [SerializeField] Animator Enemy;
    bool isWalking = false;
    bool isRunning = false;
    bool isDead = false;

    BoxCollider EnemyHand;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange = false, playerInAttackRange = false;

    AudioSource enemyAudio;
    [SerializeField] AudioClip dying;
    private void Start()
    {
        player = GameObject.Find("Police Man").transform;

        agent = GetComponent<NavMeshAgent>();
        Enemy = GetComponent<Animator>();
        EnemyHand = GetComponentInChildren<BoxCollider>();

        playerInSightRange = false;
        playerInAttackRange = false;

        enemyAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Check for sight range
        playerInSightRange = Vector3.Distance(transform.position, player.position) <= sightRange;


        // If player is within sight range, check if they're also within attack range
        if (playerInSightRange)
        {
            playerInAttackRange = Vector3.Distance(transform.position, player.position) <= attackRange;


            if (playerInAttackRange)
            {
                // If player is within attack range, stop patrolling and attack
                if (!isDead)
                {
                    Attack();
                    return;
                }
            }
            else
            {
                // If player is not within attack range, chase

                if (!isDead)
                {
                    ChasePlayer();
                    return;
                }

            }
        }
        else
        {
            // If player is not within sight range, resume patrolling
            if (!isDead)
            {
                isRunning = false;
                Enemy.SetBool("isRunning", isRunning);
                Patroling();
            }

        }
    }

    private void Patroling()
    {
        isWalking = true;
        Enemy.SetBool("isWalking", isWalking);
        agent.speed = 1.5f;

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(player.position, walkPointRange, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                agent.SetDestination(point);
            }

        }
    }

    bool RandomPoint(Vector3 center_of_player, float range, out Vector3 result)
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        NavMeshHit hit;

        if (NavMesh.SamplePosition(walkPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    private void ChasePlayer()
    {
        isRunning = true;
        isWalking = true;
        Enemy.SetBool("isWalking", isWalking);
        Enemy.SetBool("isRunning", isRunning);

        agent.SetDestination(player.position);
        agent.speed = 3.0f;
    }

    void Attack()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0;

        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;

        Enemy.SetTrigger("isAttacking");
        agent.SetDestination(transform.position);
    }

    void TakeDamage()
    {
        health -= DamagePerSecond * Time.deltaTime;
        if (health <= 0 && !isDead)
        {
            EnemyDead();
            GameObject.Find("PoliceArm").SendMessage("EnemyKilled");

        }
    }

    void EnemyDead()
    {
        isDead = true;
        isWalking = isRunning = false;
        enemyAudio.PlayOneShot(dying);
        Enemy.SetBool("isWalking", isWalking);
        Enemy.SetBool("isRunning", isRunning);
        Enemy.SetBool("isDead", isDead);

        Destroy(gameObject, 3f);

    }

    void EnabledAttack()
    {
        EnemyHand.enabled = true;
    }

    void DisabledAttack()
    {
        EnemyHand.enabled = false;
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}