using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XtremeFPS.WeaponSystem;

public class HealthKit : MonoBehaviour
{
    [SerializeField] private float healAmount = 25f; // ���������� ��������, ������� ��������������� �������

    private void OnTriggerEnter(Collider other)
    {
        // ���������, �������� �� ������ ������� � ����������� ��������
        HealthPlayer playerHealth = other.GetComponent<HealthPlayer>();
        if (playerHealth != null)
        {
            // ��������������� �������� ������
            playerHealth.Heal(healAmount);

            // ���������� ������� ����� �������������
            Destroy(gameObject);
        }
    }
}
