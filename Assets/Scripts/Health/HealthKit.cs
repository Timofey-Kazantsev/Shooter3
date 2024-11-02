using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XtremeFPS.WeaponSystem;

public class HealthKit : MonoBehaviour
{
    [SerializeField] private float healAmount = 25f; // Количество здоровья, которое восстанавливает аптечка

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, является ли объект игроком с компонентом здоровья
        HealthPlayer playerHealth = other.GetComponent<HealthPlayer>();
        if (playerHealth != null)
        {
            // Восстанавливаем здоровье игрока
            playerHealth.Heal(healAmount);

            // Уничтожаем аптечку после использования
            Destroy(gameObject);
        }
    }
}
