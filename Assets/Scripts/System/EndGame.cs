using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using XtremeFPS.WeaponSystem;

public class EndGame : MonoBehaviour
{
    private int aliveBotsCount;

    void Start()
    {
        UpdateAliveBotsCount();
    }

    public void CheckAliveBots()
    {
        UpdateAliveBotsCount();
        if (aliveBotsCount <= 0)
        {
            EndLevel();
        }
    }

    private void UpdateAliveBotsCount()
    {
        aliveBotsCount = FindObjectsOfType<EnemyWeapon>().Length;
    }

    private void EndLevel()
    {
        SceneManager.LoadScene("Menu");
    }
}

