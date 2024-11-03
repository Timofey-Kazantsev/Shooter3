using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using XtremeFPS.PoolingSystem;

namespace XtremeFPS.WeaponSystem
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class SC_NPCEnemy : MonoBehaviour
    {
        public float attackDistance = 3f;
        public float movementSpeed = 4f;
        public float npcHP = 100;
        public float npcDamage = 5;
        public float attackRate = 0.5f;
        public Transform firePoint;
        public GameObject npcDeadPrefab;
        public GameObject fireProjectile;  // ������ ����
        public GameObject bloodPrefab;  // ������ ������� �����
        private bool alreadyAttacked = false;

        [HideInInspector] public Transform playerTransform;
        private NavMeshAgent agent;
        private float nextAttackTime = 0;

        void Start()
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            agent = GetComponent<NavMeshAgent>();
            agent.stoppingDistance = attackDistance;
            agent.speed = movementSpeed;

            if (GetComponent<Rigidbody>())
            {
                GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        void Update()
        {
            // ���������, ��������� �� ���� � �������� ��������� �����
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer <= attackDistance)
            {
                // ����� AttackPlayer(), ���� ������ ����� ��� ��������� �����
                if (!alreadyAttacked && Time.time >= nextAttackTime)
                {
                    AttackPlayer();
                    alreadyAttacked = true;
                    nextAttackTime = Time.time + attackRate;

                    // ��������� ������ ��� ������ ��������� "��� ��������"
                    Invoke(nameof(ResetAttack), attackRate);
                }
            }

            // ����������� ����� � ������
            agent.destination = playerTransform.position;
            transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));
        }


        private void AttackPlayer()
        {
            // ������� ���� � ������ ����������� �� ������
            GameObject bullet = PoolManager.Instance.SpawnObject(fireProjectile, firePoint.position, firePoint.rotation);
            if (bullet != null)
            {
                ParabolicBullet bulletScript = bullet.GetComponent<ParabolicBullet>();
                if (bulletScript != null)
                {
                    // ������������ ����������� �� ������
                    Vector3 directionToPlayer = (playerTransform.position - firePoint.position).normalized;

                    // �������������� ���� � ������������ ������������
                    bulletScript.Initialize(
                        firePoint,  // ��������� ������� ����
                        32f,        // �������� ����
                        npcDamage,  // ����
                        9.81f,      // ����������
                        5f,         // ����� ����� ����
                        bloodPrefab // ������ �����
                    );

                    firePoint.LookAt(playerTransform.position); // ��������� ����������� firePoint �� ������

                    if (bullet != null)
                    {
                        Rigidbody rb = bullet.GetComponent<Rigidbody>();
                        rb.velocity = Vector3.zero; // ����� �������� ����
                        rb.AddForce(firePoint.forward * 32f, ForceMode.Impulse);
                    }

                    // ������������� ���������� ���� �� ������
                    bullet.transform.rotation = Quaternion.LookRotation(directionToPlayer);
                }
            }
        }


        private void ResetAttack()
        {
            alreadyAttacked = false;
        }

        public void TakeDamage(float amount)
        {
            npcHP -= amount;
            if (npcHP <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (npcDeadPrefab != null)
            {
                Instantiate(npcDeadPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
