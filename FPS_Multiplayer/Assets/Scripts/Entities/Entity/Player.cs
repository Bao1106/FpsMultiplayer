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
            InitObserver();
            base.Awake();
        }

        private void InitObserver()
        {
            EntityHealth = new Observer<int>(MaxHealth);
            IsDamaged = new Observer<bool>(false);

            StaticEvents.PlayerHealth = EntityHealth;
            StaticEvents.IsDamaged = IsDamaged;
        }
        
        private void OnDamage(int damage)
        {
            if (EntityHealth.Value > 0)
            {
                EntityHealth.Value -= damage;
                IsDamaged.Value = true;
            }
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
