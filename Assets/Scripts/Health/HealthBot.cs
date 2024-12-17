using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XtremeFPS.WeaponSystem {
    public class HealthBot : MonoBehaviour {
        public float health = 100f;

        [SerializeField] private EnemyView _view;
        [SerializeField] private RagdollHandler _ragdollHandler;
        [SerializeField] private GameObject _weapon;
        [SerializeField] private Collider _capsule;
        [SerializeField] private SC_NPCEnemy _enemy;
        public enum State { Patrolling, Chasing, Attacking, Dead }
        EndGame endGame = new EndGame();

        void Awake() 
        {
            _ragdollHandler.Initialize();
        }

        public void Damage(float damage) {
            health -= damage;
            if (health <= 0) {
                Dead();
            }
        }

        public void Dead()
        {
            _view.DisableAnimator();
            _ragdollHandler.Enable();
            _enemy.currentState = (SC_NPCEnemy.State)State.Dead;
            Destroy(_weapon);
            Destroy(_capsule);
            endGame.CheckAliveBots();
        }
    }
}
    