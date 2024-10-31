using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XtremeFPS.WeaponSystem;

namespace XtremeFPS.FPSController
{
    public class Grenade : MonoBehaviour
    {
        [SerializeField] private GameObject explosionEffectPrefab; // ������ ������� ������
        [SerializeField] private AudioClip explosionSound;         // ���� ������
        [SerializeField] private float explosionRadius;            // ������ ������
        [SerializeField] private float explosionDamage;            // ���� �� ������
        [SerializeField] private float explosionDelay;             // �������� ������
        [SerializeField] private bool showExplosionRadiusInGame = true; // ���������� ������ � ����

        private void Start()
        {
            Invoke(nameof(Explode), explosionDelay);
        }

        private void Explode()
        {
            // ������� ������ ������
            if (explosionEffectPrefab != null)
            {
                GameObject explosionEffect = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
                Destroy(explosionEffect, 2f);
            }

            // ����������� ���� ������
            if (explosionSound != null)
            {
                AudioSource.PlayClipAtPoint(explosionSound, transform.position);
            }

            // ������� ���� ���� �������� � ������� ������
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (Collider nearbyObject in colliders)
            {
                HealthBot health = nearbyObject.GetComponent<HealthBot>();
                HealthPlayer health_player = nearbyObject.GetComponent<HealthPlayer>();
                if (health != null)
                {
                    health.Damage(explosionDamage);
                }
                if (health_player != null)
                {
                    health_player.Damage(explosionDamage);
                }
            }

            // ���������� ������� ����� ������
            Destroy(gameObject);
        }

        // ��������� ���������� ������
        public void SetExplosionParameters(float radius, float damage, float delay)
        {
            explosionRadius = radius;
            explosionDamage = damage;
            explosionDelay = delay;
        }

        // ������������ ������� ������ � ���������
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }

        // ������������ ������� ������ � ����� ���� (���� ��������)
        private void OnDrawGizmos()
        {
            if (showExplosionRadiusInGame)
            {
                Gizmos.color = new Color(1, 0, 0, 0.3f); // �������������� �������
                Gizmos.DrawSphere(transform.position, explosionRadius);
            }
        }
    }
}
