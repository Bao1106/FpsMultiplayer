using Events;
using GOAP.Config;
using Services;
using UnityEngine;

namespace Entities.Entity
{
    public class Player : Base.Entity
    {
        [SerializeField] private AttackConfig attackConfig;
        
        protected override void Awake()
        {
            MaxHealth = 100;
            entityHealth = new Observer<int>(MaxHealth);

            StaticEvents.PlayerHealth = entityHealth;
            base.Awake();
        }

        private void OnDamage(int damage)
        {
            if(entityHealth.Value > 0)
                entityHealth.Value -= damage;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                OnDamage(attackConfig.normalAttackCost);
            }
        }
    }
}
