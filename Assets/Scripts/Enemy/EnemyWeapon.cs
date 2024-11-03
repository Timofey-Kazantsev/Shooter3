using UnityEngine;
using XtremeFPS.PoolingSystem;

namespace XtremeFPS.WeaponSystem
{
    public class EnemyWeapon : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab; // ������ ����
        [SerializeField] private Transform firePoint; // ����� ��������
        [SerializeField] private float fireRate = 1f; // ������� ���������
        private float nextFireTime = 0f;

        public float bulletSpeed = 20f;
        public float bulletDamage = 10f;
        public float bulletLifetime = 3f;
        public float gravityEffect = 1f;

        private void Update()
        {
            // ���� ������ ����� ��� ��������
            if (Time.time >= nextFireTime)
            {
                Fire();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }

        private void Fire()
        {
            // ����� ���� �� ���� ��������
            GameObject bullet = PoolManager.Instance.SpawnObject(bulletPrefab, firePoint.position, firePoint.rotation);
            if (bullet != null)
            {
                // ������������� ���������� ����
                EnemyBullet bulletComponent = bullet.GetComponent<EnemyBullet>();
                bulletComponent.Initialize(firePoint.forward, bulletSpeed, bulletDamage, gravityEffect, bulletLifetime);
            }
        }
    }
}
