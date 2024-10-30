using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XtremeFPS.WeaponSystem
{
    public class HealthBot : MonoBehaviour
    {
        [SerializeField] private GameObject owner;
        public float health = 100f;
        // Start is called before the first frame update
        void Awake()
        {
            owner = gameObject;
        }

        public void Damage(float damage)
        {
            health -= damage;
            Debug.Log(owner);
            if (owner.name == "Player Armature")
            {
                Debug.Log("123123");
            }
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
    