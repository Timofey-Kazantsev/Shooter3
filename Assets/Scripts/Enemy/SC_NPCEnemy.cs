using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace XtremeFPS.WeaponSystem
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class SC_NPCEnemy : MonoBehaviour
    {
        public float patrolSpeed = 2f;
        public float chaseSpeed = 5f;
        public float attackDistance = 1f;
        public float chaseRange = 10f;
        public float npcHP = 100;
        public float attackRate = 0.5f;
        public Transform firePoint;
        public GameObject npcDeadPrefab;
        public Transform[] patrolPoints;
        public Animator animator;
        public Transform playerTransform;
        public GameObject enemyWeaponGO;

        private NavMeshAgent agent;
        private bool alreadyAttacked = false;
        private float nextAttackTime = 0f;
        private EnemyWeapon enemyWeapon;

        private enum State { Patrolling, Chasing, Attacking }
        private State currentState;

        private void Start()
        {
            enemyWeapon = enemyWeaponGO.GetComponent<EnemyWeapon>();
            if (enemyWeapon == null)
            {
                Debug.LogError("EnemyWeapon component not found on enemy!");
            }

            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            agent = GetComponent<NavMeshAgent>();
            agent.stoppingDistance = attackDistance;

            currentState = State.Patrolling;
            agent.SetDestination(patrolPoints[0].position);
        }

        private void Update()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            switch (currentState)
            {
                case State.Patrolling:
                    HandlePatrolState(distanceToPlayer);
                    break;
                case State.Chasing:
                    HandleChaseState(distanceToPlayer);
                    break;
                case State.Attacking:
                    HandleAttackState(distanceToPlayer);
                    break;
            }
        }

        private void HandlePatrolState(float distanceToPlayer)
        {
            Debug.Log("Patrol");

            agent.speed = patrolSpeed;
            animator.SetBool("IsPatrolling", true);
            animator.SetBool("IsChasing", false);
            animator.SetBool("IsAttacking", false);

            if (distanceToPlayer <= chaseRange)
            {
                currentState = State.Chasing;
                return;
            }

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                agent.SetDestination(patrolPoints[Random.Range(0, patrolPoints.Length)].position);
            }
        }

        private void HandleChaseState(float distanceToPlayer)
        {
            Debug.Log("Chase");

            agent.speed = chaseSpeed;
            agent.SetDestination(playerTransform.position);

            animator.SetBool("IsPatrolling", false);
            animator.SetBool("IsChasing", true);
            animator.SetBool("IsAttacking", false);

            if (distanceToPlayer <= attackDistance)
            {
                currentState = State.Attacking;
            }
            else if (distanceToPlayer > chaseRange)
            {
                currentState = State.Patrolling;
                agent.SetDestination(patrolPoints[Random.Range(0, patrolPoints.Length)].position);
            }
        }

        private void HandleAttackState(float distanceToPlayer)
        {
            Debug.Log("Attack");
            agent.isStopped = true;
            transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));

            animator.SetBool("IsPatrolling", false);
            animator.SetBool("IsChasing", false);
            animator.SetBool("IsAttacking", true);

            if (!alreadyAttacked && Time.time >= nextAttackTime)
            {
                AttackPlayer();
                alreadyAttacked = true;
                nextAttackTime = Time.time + attackRate;
                Invoke(nameof(ResetAttack), attackRate);
            }

            if (distanceToPlayer > attackDistance)
            {
                agent.isStopped = false;
                currentState = State.Chasing;
            }
        }

        private void AttackPlayer()
        {
            if (enemyWeapon != null)
            {
                enemyWeapon.Fire();
            }
        }

        private void ResetAttack()
        {
            alreadyAttacked = false;
        }
    }
}
