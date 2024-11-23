using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XtremeFPS.WeaponSystem;

public class EndGame : MonoBehaviour
{
    [SerializeField] Exit exit;


    // Update is called once per frame
    void Update()
    {
        var enemyes = FindObjectsOfType<SC_NPCEnemy>();
        Debug.Log(enemyes.Length);
        if(enemyes.Length <= 0)
        {
            exit.QuitGame();
        }
    }
}
