using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace XtremeFPS.WeaponSystem
{
    public class HealthPlayer : MonoBehaviour
    {
        public float health = 100f;

        public void Damage(float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
}