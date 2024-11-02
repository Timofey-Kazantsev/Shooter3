using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace XtremeFPS.WeaponSystem
{
    public class HealthPlayer : MonoBehaviour
    {
        public float health = 100f;
        public float maxHealth = 100f;

        public void Damage(float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                SceneManager.LoadScene("Menu");
            }
        }

        public void Heal(float healAmount)
        {
            health = Mathf.Min(health + healAmount, maxHealth); // Не превышаем максимальное здоровье
            Debug.Log("Здоровье восстановлено. Текущее здоровье: " + health);
        }
    }
}