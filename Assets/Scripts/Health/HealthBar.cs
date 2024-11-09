using UnityEngine;
using UnityEngine.UI;
using XtremeFPS.WeaponSystem;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider; // Ссылка на UI Slider
    private HealthPlayer playerHealth; // Ссылка на компонент HealthPlayer

    void Start()
    {
        // Найти компонент HealthPlayer на объекте игрока
        playerHealth = FindObjectOfType<HealthPlayer>();

        // Инициализировать максимум здоровья
        if (playerHealth != null && healthSlider != null)
        {
            healthSlider.maxValue = playerHealth.maxHealth;
            healthSlider.value = playerHealth.health;
        }
    }

    void Update()
    {
        // Обновить значение слайдера в соответствии с текущим здоровьем
        if (playerHealth != null && healthSlider != null)
        {
            healthSlider.value = playerHealth.health;
        }
    }
}
