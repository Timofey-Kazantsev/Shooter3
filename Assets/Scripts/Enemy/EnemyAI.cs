using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public float attackRange = 2f;
    public float chaseRange = 10f;
    public Transform[] patrolPoints;
    public Animator animator;

    private Transform player;
    private NavMeshAgent agent;
    private int currentPatrolIndex;
    private bool isPatrolling = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        currentPatrolIndex = 0;

        Patrol();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            Attack();
        }
        else if (distanceToPlayer <= chaseRange)
        {
            Chase();
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (!isPatrolling)
        {
            isPatrolling = true;
            agent.speed = patrolSpeed;
            agent.isStopped = false;

            // ”станавливаем параметры анимации
            animator.SetBool("isPatrolling", true);
            animator.SetBool("isChasing", false);
            animator.SetBool("isAttacking", false);
        }

        // ѕереход к следующей точке патрулировани€, если достигнута текуща€
        if (agent.remainingDistance <= agent.stoppingDistance && patrolPoints.Length > 0)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }


        Debug.Log("Patrol");
    }

    private void Chase()
    {
        if (isPatrolling)
        {
            isPatrolling = false;
            agent.speed = chaseSpeed;
            agent.isStopped = false;

            // ”станавливаем параметры анимации
            animator.SetBool("isPatrolling", false);
            animator.SetBool("isChasing", true);
            animator.SetBool("isAttacking", false);
        }

        Debug.Log("Chasing");
        agent.SetDestination(player.position);
    }

    private void Attack()
    {
        agent.isStopped = true;

        // ”станавливаем параметры анимации
        animator.SetBool("isPatrolling", false);
        animator.SetBool("isChasing", false);
        animator.SetBool("isAttacking", true);


        Debug.Log("Attack");

        
    }
}
