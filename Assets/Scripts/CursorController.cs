using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace XtremeFPS.WeaponSystem
{
    public class CursorController : MonoBehaviour
    {
        private void OnEnable()
        {
            // Подписываемся на событие загрузки сцены
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            // Отписываемся от события загрузки сцены, чтобы избежать ошибок
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "Menu")
            {
                // Если загружена сцена меню, делаем курсор видимым и свободным
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else if (scene.name == "Stroika")
            {
                // Если загружена сцена игры, скрываем курсор и блокируем его
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}