using System;
using GOAP.Sensors;
using Interfaces;
using Services;
using Services.DependencyInjection;
using SO;
using UnityEngine;
using UnityEngine.AI;

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
        
        public Observer<int> EnemyHealth { get; set; }

        public void OnDamaged(int damage)
        {
            EnemyHealth.Value -= damage;
            if (EnemyHealth.Value == 0)
            {
                OnEnemyDead?.Invoke();
            }
        }

        
    }
}
