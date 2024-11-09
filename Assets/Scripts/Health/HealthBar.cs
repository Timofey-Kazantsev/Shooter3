using UnityEngine;
using UnityEngine.UI;
using XtremeFPS.WeaponSystem;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider; // ������ �� UI Slider
    private HealthPlayer playerHealth; // ������ �� ��������� HealthPlayer

    void Start()
    {
        // ����� ��������� HealthPlayer �� ������� ������
        playerHealth = FindObjectOfType<HealthPlayer>();

        // ���������������� �������� ��������
        if (playerHealth != null && healthSlider != null)
        {
            healthSlider.maxValue = playerHealth.maxHealth;
            healthSlider.value = playerHealth.health;
        }
    }

    void Update()
    {
        // �������� �������� �������� � ������������ � ������� ���������
        if (playerHealth != null && healthSlider != null)
        {
            healthSlider.value = playerHealth.health;
        }
    }
}
