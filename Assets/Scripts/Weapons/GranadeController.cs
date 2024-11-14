using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace XtremeFPS.FPSController
{
    public class GranadeController : MonoBehaviour
    {
        [SerializeField] private GameObject grenadePrefab;          // Префаб гранаты
        [SerializeField] private Transform throwPoint;              // Точка броска
        [SerializeField] private float throwForce = 10f;            // Сила броска
        [SerializeField] private int maxGrenades = 3;               // Максимальное количество гранат
        [SerializeField] private float explosionRadius = 5f;        // Радиус взрыва гранаты
        [SerializeField] private float explosionDamage = 50f;       // Урон гранаты
        [SerializeField] private float explosionDelay = 2f;         // Задержка взрыва
        [SerializeField] private TextMeshProUGUI grenadeCounterText; // UI элемент для отображения количества гранат

        private int currentGrenades;                               // Текущее количество гранат

        public int CurrentGrenades => currentGrenades;             // Свойство для доступа к текущему количеству гранат
        public int MaxGrenades => maxGrenades;                     // Свойство для доступа к максимальному количеству гранат

        private void Start()
        {
            currentGrenades = maxGrenades;                         // Изначально у игрока максимум гранат
            UpdateGrenadeUI();                                     // Обновляем UI
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G) && currentGrenades > 0)
            {
                ThrowGrenade();
            }
        }

        private void ThrowGrenade()
        {
            Vector3 spawnPosition = throwPoint != null ? throwPoint.position : transform.position;

            // Создаем гранату
            GameObject grenade = Instantiate(grenadePrefab, spawnPosition, Quaternion.identity);

            // Передаем параметры в скрипт гранаты
            Grenade grenadeScript = grenade.GetComponent<Grenade>();
            grenadeScript.SetExplosionParameters(explosionRadius, explosionDamage, explosionDelay);

            // Добавляем силу броска
            Rigidbody rb = grenade.GetComponent<Rigidbody>();
            Vector3 throwDirection = throwPoint != null ? throwPoint.forward : transform.forward;
            rb.AddForce(throwDirection * throwForce, ForceMode.VelocityChange);

            currentGrenades--;               // Уменьшаем количество гранат
            UpdateGrenadeUI();               // Обновляем UI
        }

        // Метод для пополнения гранат
        public void AddGrenade(int count)
        {
            currentGrenades = Mathf.Min(currentGrenades + count, maxGrenades);
            UpdateGrenadeUI();               // Обновляем UI
        }

        // Метод для обновления UI текста с количеством гранат
        private void UpdateGrenadeUI()
        {
            if (grenadeCounterText != null)
            {
                grenadeCounterText.text = $"{currentGrenades}";
            }
        }
    }
}
