using System;
using Interfaces;
using SO;
using UnityEngine;

namespace Entities.Base
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        [SerializeField] protected BulletConfig bulletConfig;

        protected Action OnEnemyDead;
        protected Animator Animator;

        protected virtual void Start()
        {
            Animator = GetComponent<Animator>();
        }
        
        public int EnemyHealth { get; set; }
        public void OnDamaged(int damage)
        {
            EnemyHealth -= damage;
            if (EnemyHealth == 0)
            {
                OnEnemyDead?.Invoke();
            }
        }

        
    }
}
