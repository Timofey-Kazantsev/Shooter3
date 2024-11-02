using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XtremeFPS.WeaponSystem;

namespace XtremeFPS.FPSController
{
    public class Grenade : MonoBehaviour
    {
        [SerializeField] private GameObject explosionEffectPrefab; // Префаб эффекта взрыва
        [SerializeField] private AudioClip explosionSound;         // Звук взрыва
        [SerializeField] private float explosionRadius;            // Радиус взрыва
        [SerializeField] private float explosionDamage;            // Урон от взрыва
        [SerializeField] private float explosionDelay;             // Задержка взрыва
        [SerializeField] private bool showExplosionRadiusInGame = true; // Отображать радиус в игре

        private void Start()
        {
            Invoke(nameof(Explode), explosionDelay);
        }

        private void Explode()
        {
            // Создаем эффект взрыва
            if (explosionEffectPrefab != null)
            {
                GameObject explosionEffect = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
                Destroy(explosionEffect, 2f);
            }

            // Проигрываем звук взрыва
            if (explosionSound != null)
            {
                AudioSource.PlayClipAtPoint(explosionSound, transform.position);
            }

            // Наносим урон всем объектам в радиусе взрыва
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

            // Уничтожаем гранату после взрыва
            Destroy(gameObject);
        }

        // Установка параметров взрыва
        public void SetExplosionParameters(float radius, float damage, float delay)
        {
            explosionRadius = radius;
            explosionDamage = damage;
            explosionDelay = delay;
        }

        // Визуализация радиуса взрыва в редакторе
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }

        // Визуализация радиуса взрыва в самой игре (если включено)
        private void OnDrawGizmos()
        {
            if (showExplosionRadiusInGame)
            {
                Gizmos.color = new Color(1, 0, 0, 0.3f); // Полупрозрачный красный
                Gizmos.DrawSphere(transform.position, explosionRadius);
            }
        }
    }
}
