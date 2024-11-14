using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace XtremeFPS.FPSController
{
    public class GranadeController : MonoBehaviour
    {
        [SerializeField] private GameObject grenadePrefab;          // ������ �������
        [SerializeField] private Transform throwPoint;              // ����� ������
        [SerializeField] private float throwForce = 10f;            // ���� ������
        [SerializeField] private int maxGrenades = 3;               // ������������ ���������� ������
        [SerializeField] private float explosionRadius = 5f;        // ������ ������ �������
        [SerializeField] private float explosionDamage = 50f;       // ���� �������
        [SerializeField] private float explosionDelay = 2f;         // �������� ������
        [SerializeField] private TextMeshProUGUI grenadeCounterText; // UI ������� ��� ����������� ���������� ������

        private int currentGrenades;                               // ������� ���������� ������

        public int CurrentGrenades => currentGrenades;             // �������� ��� ������� � �������� ���������� ������
        public int MaxGrenades => maxGrenades;                     // �������� ��� ������� � ������������� ���������� ������

        private void Start()
        {
            currentGrenades = maxGrenades;                         // ���������� � ������ �������� ������
            UpdateGrenadeUI();                                     // ��������� UI
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

            // ������� �������
            GameObject grenade = Instantiate(grenadePrefab, spawnPosition, Quaternion.identity);

            // �������� ��������� � ������ �������
            Grenade grenadeScript = grenade.GetComponent<Grenade>();
            grenadeScript.SetExplosionParameters(explosionRadius, explosionDamage, explosionDelay);

            // ��������� ���� ������
            Rigidbody rb = grenade.GetComponent<Rigidbody>();
            Vector3 throwDirection = throwPoint != null ? throwPoint.forward : transform.forward;
            rb.AddForce(throwDirection * throwForce, ForceMode.VelocityChange);

            currentGrenades--;               // ��������� ���������� ������
            UpdateGrenadeUI();               // ��������� UI
        }

        // ����� ��� ���������� ������
        public void AddGrenade(int count)
        {
            currentGrenades = Mathf.Min(currentGrenades + count, maxGrenades);
            UpdateGrenadeUI();               // ��������� UI
        }

        // ����� ��� ���������� UI ������ � ����������� ������
        private void UpdateGrenadeUI()
        {
            if (grenadeCounterText != null)
            {
                grenadeCounterText.text = $"{currentGrenades}";
            }
        }
    }
}
