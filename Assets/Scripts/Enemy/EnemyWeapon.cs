using UnityEngine;
using XtremeFPS.PoolingSystem;

namespace XtremeFPS.WeaponSystem
{
    public class EnemyWeapon : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab; // ������ ����
        [SerializeField] private GameObject muzzleFlashPrefab; // ������ ������� �������� (ParticleSystem)
        [SerializeField] private Transform firePoint; // ����� ��������
        [SerializeField] private float fireRate = 1f; // ������� ���������
        [SerializeField] private AudioClip fireSound; // ���� ��������
        private AudioSource audioSource;

        private float nextFireTime = 0f;
        public float bulletSpeed = 20f;
        public float bulletDamage = 10f;
        public float bulletLifetime = 3f;
        public float gravityEffect = 1f;

        private void Awake()
        {
            // �������� ��� ��������� AudioSource �� ���� ������
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        public void Fire()
        {
            // �������� ����
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            if (bullet != null)
            {
                EnemyBullet bulletComponent = bullet.GetComponent<EnemyBullet>();
                bulletComponent.Initialize(firePoint.forward, bulletSpeed, bulletDamage, gravityEffect, bulletLifetime);
            }

            // �������� ������� �������� (ParticleSystem)
            if (muzzleFlashPrefab != null)
            {
                GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
                ParticleSystem particleSystem = muzzleFlash.GetComponent<ParticleSystem>();
                if (particleSystem != null)
                {
                    particleSystem.Play();
                    Destroy(muzzleFlash, particleSystem.main.duration); // ������� ������ ����� ��������� ��������
                }
            }

            // ��������������� ����� ��������
            if (fireSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(fireSound);
            }
        }
    }
}
