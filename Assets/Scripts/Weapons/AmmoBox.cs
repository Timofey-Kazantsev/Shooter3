using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XtremeFPS.FPSController;
using XtremeFPS.WeaponSystem;

public class AmmoBox : MonoBehaviour
{
    [SerializeField] private int ammoInBox = 100; 
    [SerializeField] private KeyCode interactKey = KeyCode.F;    
    [SerializeField] private float interactionDistance = 3f;
    private int bulletsToAdd;
    private int ammoInBoxForAK;
    private int ammoInBoxForShotgun;
    private int ammoInBoxForPistol;

    void Start()
    {
        ammoInBoxForAK = ammoInBox;
        ammoInBoxForPistol = ammoInBox;
        ammoInBoxForShotgun = ammoInBox;
    }
    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");
        GameObject gun = GameObject.FindGameObjectWithTag("Gun");
        GameObject weaponSlot = GameObject.Find("Player Parent/Player Armature/Camera Holder/Camera Follow/Weapon Holder/Weapon Recoils");
        UniversalWeaponSystem gunSystem = weaponSlot.transform.GetChild(0).GetComponent<UniversalWeaponSystem>();

        if (player != null && Vector3.Distance(transform.position,
            player.transform.position) <= interactionDistance && gun != null)
        {
            
            if (Input.GetKeyDown(interactKey) && gunSystem != null)
            {
                if (ammoInBox > 0 && gunSystem.totalBullets < gunSystem.maxAmmo)
                {
                    bulletsToAdd = gunSystem.maxAmmo - gunSystem.totalBullets;
                    if (gunSystem.name == "AK-47")
                    {
                        if(ammoInBoxForAK > 0)
                        {
                            if (ammoInBoxForAK <= bulletsToAdd)
                            {
                                bulletsToAdd = ammoInBoxForAK;
                                ammoInBoxForAK -= bulletsToAdd;
                                gunSystem.addAmmo(bulletsToAdd);
                                Debug.Log($"Пополнено {bulletsToAdd} патрон. Осталось {ammoInBoxForAK}");
                            }
                            else
                            {
                                ammoInBoxForAK -= bulletsToAdd;
                                gunSystem.addAmmo(bulletsToAdd);
                                Debug.Log($"Пополнено {bulletsToAdd} патрон. Осталось {ammoInBoxForAK}");
                            }
                        }
                    }
                    else if(gunSystem.name == "870_Shotgun")
                    {
                        if(ammoInBoxForShotgun > 0)
                        {
                            if (ammoInBoxForShotgun <= bulletsToAdd)
                            {
                                bulletsToAdd = ammoInBoxForShotgun;
                                ammoInBoxForShotgun -= bulletsToAdd;
                                gunSystem.addAmmo(bulletsToAdd);
                                Debug.Log($"Пополнено {bulletsToAdd} патрон. Осталось {ammoInBoxForShotgun}");
                            }
                            else
                            {
                                ammoInBoxForShotgun -= bulletsToAdd;
                                gunSystem.addAmmo(bulletsToAdd);
                                Debug.Log($"Пополнено {bulletsToAdd} патрон. Осталось {ammoInBoxForShotgun}");
                            }
                        }
                    }
                    else if(gunSystem.name == "Pistol")
                    {
                        if (ammoInBoxForPistol > 0)
                        {
                            if (ammoInBoxForPistol <= bulletsToAdd)
                            {
                                bulletsToAdd = ammoInBoxForPistol;
                                ammoInBoxForPistol -= bulletsToAdd;
                                gunSystem.addAmmo(bulletsToAdd);
                                Debug.Log($"Пополнено {bulletsToAdd} патрон. Осталось {ammoInBoxForPistol}");
                            }
                            else
                            {
                                ammoInBoxForPistol -= bulletsToAdd;
                                gunSystem.addAmmo(bulletsToAdd);
                                Debug.Log($"Пополнено {bulletsToAdd} патрон. Осталось {ammoInBoxForPistol}");
                            }

                        }

                    }
                    if (ammoInBoxForAK <=0 && ammoInBoxForPistol <=0 && ammoInBoxForShotgun <=0)
                    {
                        Destroy(gameObject);
                    }
                }
                else
                {
                    Debug.Log("В ящике больше нет патрон или у игрока максимальное количество патрон.");
                }
            }
        }
    }
}
