using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XtremeFPS.WeaponSystem;

public class HealthKit : MonoBehaviour
{
    [SerializeField] private float healInKit = 100;
    [SerializeField] private KeyCode interactKey = KeyCode.F;
    [SerializeField] private float interactionDistance = 3f;
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

    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");
        GameObject gun = GameObject.FindGameObjectWithTag("Gun");
        GameObject weaponSlot = GameObject.Find("Player Parent/Player Armature/Camera Holder/Camera Follow/Weapon Holder/Weapon Recoils");


        if (player != null && Vector3.Distance(transform.position,
            player.transform.position) <= interactionDistance && gun != null)
        {
            if (Input.GetKeyDown(interactKey) && player.GetComponent<HealthPlayer>().health != 100)
            {
                HealthPlayer playerHealth = player.GetComponent<HealthPlayer>();
                if (playerHealth != null)
                {
                    playerHealth.Heal(healAmount);
                    healInKit -= healAmount;
                    if(healInKit <= 0)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}

