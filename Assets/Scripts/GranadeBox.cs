using UnityEngine;

namespace XtremeFPS.FPSController
{
    public class GrenadeBox : MonoBehaviour
    {
        [SerializeField] private int grenadesInBox = 3; // ���������� ������ � �����
        [SerializeField] private KeyCode interactKey = KeyCode.E; // ������� ��� ��������������
        [SerializeField] private float interactionDistance = 3f; // ������������ ���������� ��� ��������������

        private void Update()
        {
            // ����� ������ �� ����
            GameObject player = GameObject.FindWithTag("Player");

            // ���������, ���������� �� ����� � ��������� �� �� � �������� ����������
            if (player != null && Vector3.Distance(transform.position, player.transform.position) <= interactionDistance)
            {
                // �������� ��������� GranadeController � ������
                GranadeController grenadeController = player.GetComponent<GranadeController>();

                // ���������, ������ �� ������� ��������������
                if (Input.GetKeyDown(interactKey) && grenadeController != null)
                {
                    // ���������, ���� �� ��� ������� � ����� � ������ �� � ������ ������������ ���������� ������
                    if (grenadesInBox > 0 && grenadeController.CurrentGrenades < grenadeController.MaxGrenades)
                    {
                        // ��������� ���� ������� �� �����
                        grenadeController.AddGrenade(1);
                        grenadesInBox--; // ��������� ���������� ������ � �����
                        Debug.Log($"����� �������� 1 �������. �������� � �����: {grenadesInBox}");

                        // ���� ������ � ����� ������ ���, ���������� ����
                        if (grenadesInBox <= 0)
                        {
                            Destroy(gameObject);
                        }
                    }
                    else
                    {
                        Debug.Log("� ����� ������ ��� ������ ��� � ������ ������������ ���������� ������.");
                    }
                }
            }
        }
    }
}
