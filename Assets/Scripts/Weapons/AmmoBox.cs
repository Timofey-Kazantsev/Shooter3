using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XtremeFPS.FPSController;
using XtremeFPS.WeaponSystem;

public class AmmoBox : MonoBehaviour
{
    [SerializeField] private int ammoInBox = 100; // Количество гранат в ящике
    [SerializeField] private KeyCode interactKey = KeyCode.F; // Клавиша для взаимодействия
    [SerializeField] private float interactionDistance = 3f;
    private int bulletsToAdd;

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");
        GameObject gun = GameObject.FindGameObjectWithTag("Gun");

        if (player != null && Vector3.Distance(transform.position,
            player.transform.position) <= interactionDistance && gun != null)
        {
            UniversalWeaponSystem gunSystem = gun.GetComponent<UniversalWeaponSystem>();
            
           
            if (Input.GetKeyDown(interactKey) && gunSystem != null)
            {
                GameObject weaponSlot = GameObject.Find("Player Amature/Camera Holder/Camera Follow/Weapon Holder/Weapon Recoils");
                Debug.Log(weaponSlot.name);
                UniversalWeaponSystem gunSys = weaponSlot.transform.GetChild(0).GetComponent<UniversalWeaponSystem>();
                if (ammoInBox > 0 && gunSystem.totalBullets < gunSystem.maxAmmo)
                {
                    bulletsToAdd = gunSystem.maxAmmo - gunSystem.totalBullets;
                    if (ammoInBox <= bulletsToAdd)
                    {
                        bulletsToAdd = ammoInBox;
                        ammoInBox -= bulletsToAdd;
                        gunSystem.addAmmo(bulletsToAdd);
                    }
                    else
                    {
                        ammoInBox -= bulletsToAdd;
                        gunSystem.addAmmo(bulletsToAdd);
                    }
                    Debug.Log($"Игрок пополнил {bulletsToAdd} патрон. Осталось в ящике: {ammoInBox}");
                    if (ammoInBox <= 0)
                    {
                        Destroy(gameObject);
                    }
                }
                else
                {
                    Debug.Log("В ящике больше нет гранат или у игрока максимальное количество гранат.");
                }
            }
        }
    }
}
