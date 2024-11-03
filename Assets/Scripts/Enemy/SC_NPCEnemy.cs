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
        public GameObject fireProjectile;  // Префаб пули
        public GameObject bloodPrefab;  // Префаб эффекта крови
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
            // Проверяем, находится ли враг в пределах дистанции атаки
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer <= attackDistance)
            {
                // Вызов AttackPlayer(), если пришло время для следующей атаки
                if (!alreadyAttacked && Time.time >= nextAttackTime)
                {
                    AttackPlayer();
                    alreadyAttacked = true;
                    nextAttackTime = Time.time + attackRate;

                    // Запускаем таймер для сброса состояния "уже атаковал"
                    Invoke(nameof(ResetAttack), attackRate);
                }
            }

            // Передвигаем врага к игроку
            agent.destination = playerTransform.position;
            transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));
        }


        private void AttackPlayer()
        {
            // Создаем пулю и задаем направление на игрока
            GameObject bullet = PoolManager.Instance.SpawnObject(fireProjectile, firePoint.position, firePoint.rotation);
            if (bullet != null)
            {
                ParabolicBullet bulletScript = bullet.GetComponent<ParabolicBullet>();
                if (bulletScript != null)
                {
                    // Рассчитываем направление на игрока
                    Vector3 directionToPlayer = (playerTransform.position - firePoint.position).normalized;

                    // Инициализируем пулю с рассчитанным направлением
                    bulletScript.Initialize(
                        firePoint,  // начальная позиция пули
                        32f,        // скорость пули
                        npcDamage,  // урон
                        9.81f,      // гравитация
                        5f,         // время жизни пули
                        bloodPrefab // эффект крови
                    );

                    firePoint.LookAt(playerTransform.position); // Обновляем направление firePoint на игрока

                    if (bullet != null)
                    {
                        Rigidbody rb = bullet.GetComponent<Rigidbody>();
                        rb.velocity = Vector3.zero; // Сброс скорости пули
                        rb.AddForce(firePoint.forward * 32f, ForceMode.Impulse);
                    }

                    // Устанавливаем ориентацию пули на игрока
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
