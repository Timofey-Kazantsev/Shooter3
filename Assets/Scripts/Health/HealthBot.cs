using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XtremeFPS.WeaponSystem
{
    public class HealthBot : MonoBehaviour
    {
        public float health = 100f;
        // Start is called before the first frame update
        void Awake()
        {
        }

        public void Damage(float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
    