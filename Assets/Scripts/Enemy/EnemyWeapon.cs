using UnityEngine;
using XtremeFPS.PoolingSystem;

namespace XtremeFPS.WeaponSystem
{
    public class EnemyWeapon : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab; // Префаб пули
        [SerializeField] private GameObject muzzleFlashPrefab; // Префаб эффекта выстрела (ParticleSystem)
        [SerializeField] private Transform firePoint; // Точка выстрела
        [SerializeField] private float fireRate = 1f; // Частота выстрелов
        [SerializeField] private AudioClip fireSound; // Звук выстрела
        private AudioSource audioSource;

        private float nextFireTime = 0f;
        public float bulletSpeed = 20f;
        public float bulletDamage = 10f;
        public float bulletLifetime = 3f;
        public float gravityEffect = 1f;

        private void Awake()
        {
            // Получаем или добавляем AudioSource на этот объект
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        public void Fire()
        {
            // Создание пули
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            if (bullet != null)
            {
                EnemyBullet bulletComponent = bullet.GetComponent<EnemyBullet>();
                bulletComponent.Initialize(firePoint.forward, bulletSpeed, bulletDamage, gravityEffect, bulletLifetime);
            }

            // Создание эффекта выстрела (ParticleSystem)
            if (muzzleFlashPrefab != null)
            {
                GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
                ParticleSystem particleSystem = muzzleFlash.GetComponent<ParticleSystem>();
                if (particleSystem != null)
                {
                    particleSystem.Play();
                    Destroy(muzzleFlash, particleSystem.main.duration); // Удаляем префаб после окончания анимации
                }
            }

            // Воспроизведение звука выстрела
            if (fireSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(fireSound);
            }
        }
    }
}
