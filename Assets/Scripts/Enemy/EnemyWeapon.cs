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
        public float gravityEffect = 0f;

        public void Fire()
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            if (bullet != null)
            {
                // ������������� ���������� ����
                EnemyBullet bulletComponent = bullet.GetComponent<EnemyBullet>();
                bulletComponent.Initialize(firePoint.forward, bulletSpeed, bulletDamage, gravityEffect, bulletLifetime);
            }
        }
    }
}
