using System;
using Entities.Base;
using GOAP.Sensors;
using SO;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Entities.Entity
{
    public class Zombie : Enemy
    {
        private static readonly int deadType = Animator.StringToHash("DeadType");
        private static readonly int dead = Animator.StringToHash("Dead");
        
        public string zombieName;
        public IPlayerSensor PlayerSensor;
        
        private void OnEnable()
        {
            OnEnemyDead += EnemyDead;
        }

        private void OnDisable()
        {
            OnEnemyDead -= EnemyDead;
        }

        private void EnemyDead()
        {
            var deadValue = Random.Range(0f, 1f);
            Animator.SetBool(dead, true);
            Animator.SetFloat(deadType, deadValue);
            Animator.speed = 2f;
        }

        protected override void Awake()
        {
            //zombieName = gameObject.name;
            playerSensor.SetKey(zombieName);
            base.Awake();
        }

        protected override void Start()
        {
            //EnemyHealth = 100;
            base.Start();
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.TryGetComponent(out Bullet bullet))
            {
                var damage = bulletConfig.GetBulletDamage(bullet.GetBullet());
                OnDamaged(damage);
            }
        }
        
        public void DestroyObject()
        {
            Destroy(gameObject);
        }
        

        public PlayerSensor GetSensor() => playerSensor;
    }
}