using System.Collections;
using UnityEngine;

namespace XtremeFPS.WeaponSystem
{
    public class EnemyBullet : MonoBehaviour
    {
        private float speed;
        private float damage;
        private float gravity;
        private float lifetime;
        private Vector3 direction;

        private float startTime;

        // ������������� ����
        public void Initialize(Vector3 direction, float speed, float damage, float gravity, float lifetime)
        {
            this.direction = direction.normalized;
            this.speed = speed;
            this.damage = damage;
            this.gravity = gravity;
            this.lifetime = lifetime;

            startTime = Time.time;
        }

        private void OnEnable()
        {
            startTime = Time.time;
        }

        private void Update()
        {
            float timeSinceStart = Time.time - startTime;
            if (timeSinceStart >= lifetime)
            {
                Destroy(gameObject); // ���������� ����, ���� ������� ����� �����
                return;
            }

            // �������� �� �������������� ����������
            Vector3 gravityVector = Vector3.down * gravity * timeSinceStart * timeSinceStart;
            transform.position += (direction * speed * Time.deltaTime) + gravityVector;

            // �������� �� ������������
            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, speed * Time.deltaTime))
            {
                if (hit.transform.TryGetComponent(out HealthPlayer player))
                {
                    player.Damage(damage); // ������� ���� ������
                }
                Destroy(gameObject); // ���������� ���� ����� ������������
            }
        }
    }
}
