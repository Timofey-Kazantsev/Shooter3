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
            // ������������� �� ������� �������� �����
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            // ������������ �� ������� �������� �����, ����� �������� ������
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "Menu")
            {
                // ���� ��������� ����� ����, ������ ������ ������� � ���������
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else if (scene.name == "Stroika")
            {
                // ���� ��������� ����� ����, �������� ������ � ��������� ���
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}