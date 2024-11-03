using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace XtremeFPS.WeaponSystem
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class SC_NPCEnemy : MonoBehaviour
    {
        // ��������� ���������
        public float patrolSpeed = 2f;
        public float chaseSpeed = 5f;
        public float attackDistance = 2f;
        public float chaseRange = 10f;
        public float npcHP = 100;
        public float attackRate = 0.5f;
        public Transform firePoint;
        public GameObject npcDeadPrefab;
        public Transform[] patrolPoints;
        public Animator animator;
        public Transform playerTransform;
        public GameObject enemyWeaponGO;


        // ��������� ����������
        private NavMeshAgent agent;
        private bool alreadyAttacked = false;
        private float nextAttackTime = 0f;
        private int currentPatrolIndex;
        private bool isPatrolling = true;
        private bool isAttacking = true;
        private EnemyWeapon enemyWeapon;

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

            currentPatrolIndex = 0;
            Patrol();


        }

        private void Update()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer <= attackDistance)
            {
                isAttacking = true;
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

        // ������� �������������� ����� �������
        private void Patrol()
        {
            Debug.Log("Patrol");
            isAttacking = false;

            if (!isPatrolling)
            {
                isPatrolling = true;
                agent.speed = patrolSpeed;

                // �������� ��������������
                animator.SetBool("isPatrolling", true);
                animator.SetBool("isChasing", false);
                animator.SetBool("isAttacking", false);
            }

            if (agent.remainingDistance <= agent.stoppingDistance && patrolPoints.Length > 0)
            {
                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
                agent.SetDestination(patrolPoints[currentPatrolIndex].position);
            }
        }

        // ������� ������������� ������
        private void Chase()
        {
            Debug.Log("Chase");

            if (isPatrolling)
            {
                isPatrolling = false;
                agent.speed = chaseSpeed;

                // �������� �������������
                animator.SetBool("isPatrolling", false);
                animator.SetBool("isChasing", true);
                animator.SetBool("isAttacking", false);
            }

            agent.SetDestination(playerTransform.position);
        }

        // ������� �����
        private void Attack()
        {
            if (isAttacking)
            {
                Debug.Log("Attack");

                agent.isStopped = true;
                transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));

                // �������� �����
                animator.SetBool("isPatrolling", false);
                animator.SetBool("isChasing", false);
                animator.SetBool("isAttacking", true);
                // �����, ���� ������ ����� �� ��������� �����
                if (!alreadyAttacked && Time.time >= nextAttackTime)
                {
                    // �������� ����� ����� ����� ��������� EnemyWeapon, ���� ������ �����
                    if (!alreadyAttacked && Time.time >= nextAttackTime)
                    {
                        AttackPlayer();
                        alreadyAttacked = true;
                        nextAttackTime = Time.time + attackRate;

                        // ���������� ���� �����
                        Invoke(nameof(ResetAttack), attackRate);
                    }
                }
            }

        }
        private void AttackPlayer()
        {
            if (enemyWeapon != null)
            {
                enemyWeapon.Fire(); // ������� ����� EnemyWeapon
            }
        }

        /*// ������� �� ������ �����
        private void FireProjectile()
        {
            if (firePoint != null)
            {
                GameObject bullet = Instantiate(Resources.Load("EnemyBullet") as GameObject, firePoint.position, firePoint.rotation);
                if (bullet != null)
                {
                    EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();
                    if (bulletScript != null)
                    {
                        Vector3 directionToPlayer = (playerTransform.position - firePoint.position).normalized;
                        bulletScript.Initialize(directionToPlayer, 32f, 10f, 9.81f, 5f);
                    }
                }
            }
        }*/

        // ����� ��������� �����
        private void ResetAttack()
        {
            alreadyAttacked = false;
        }
    }
}
