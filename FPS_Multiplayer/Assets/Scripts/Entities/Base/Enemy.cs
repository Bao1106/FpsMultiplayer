using System;
using GOAP.Sensors;
using Interfaces;
using SO;
using UnityEngine;

namespace Entities.Base
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        [SerializeField] protected BulletConfig bulletConfig;
        [SerializeField] protected PlayerSensor playerSensor;

        protected Action OnEnemyDead;
        protected Animator Animator;

        protected virtual void Awake() { }

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
