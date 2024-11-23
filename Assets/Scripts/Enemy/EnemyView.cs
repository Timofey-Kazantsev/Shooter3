using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XtremeFPS.WeaponSystem
{
    public class EnemyView : MonoBehaviour
    {
        private const string IsPatrollingKey = "IsPatrolling";
        private const string IsChasingKey = "IsChasing";
        private const string IsAttackingKey = "IsAttacking";

        [SerializeField] private Animator _animator;

        public void DisableAnimator() => _animator.enabled = false;
        public void EnableAnimator() => _animator.enabled = true;
    }
}