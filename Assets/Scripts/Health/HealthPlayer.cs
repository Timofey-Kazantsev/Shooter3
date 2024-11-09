using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace XtremeFPS.WeaponSystem
{
    public class HealthPlayer : MonoBehaviour
    {
        public float health = 100f;
        public float maxHealth = 100f;

        private HealthBar healthBar; // ������ �� HealthBar

        void Start()
        {
            healthBar = FindObjectOfType<HealthBar>();
            UpdateHealthBar();
        }

        public void Damage(float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                SceneManager.LoadScene("Menu");
            }
            UpdateHealthBar();
        }

        public void Heal(float healAmount)
        {
            health = Mathf.Min(health + healAmount, maxHealth); // �� ��������� ������������ ��������
            Debug.Log("�������� �������������. ������� ��������: " + health);
            UpdateHealthBar();
        }

        private void UpdateHealthBar()
        {
            // ��������� �������� �������� � HealthBar
            if (healthBar != null)
            {
                healthBar.healthSlider.value = health;
            }
        }
    }
}
