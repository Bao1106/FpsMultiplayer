using GOAP.Config;
using Photon.Pun;
using Services;
using UnityEngine;

namespace Entities.Entity
{
    public class GamePlayer : Base.Entity
    {
        [SerializeField] private AttackConfig attackConfig;

        private PhotonView photonView;
        public PhotonView PhotonView => photonView;
        
        protected override void Awake()
        {
            photonView = GetComponent<PhotonView>();
            
            MaxHealth = 100;
            InitObserver();
            base.Awake();
        }

        private void InitObserver()
        {
            EntityHealth = new Observer<int>(MaxHealth);
            IsDamaged = new Observer<bool>(false);
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
