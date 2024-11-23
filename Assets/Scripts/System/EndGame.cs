using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XtremeFPS.WeaponSystem;

public class EndGame : MonoBehaviour
{
    void Update()
    {
        var enemyes = FindObjectsOfType<EnemyWeapon>();
        Debug.Log(enemyes.Length);
        if(enemyes.Length <= 0)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
