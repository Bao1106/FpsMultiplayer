using GOAP.Config;
using Infima_Games.Low_Poly_Shooter_Pack.Code.Interface;
using Photon.Pun;
using Services;
using UnityEngine;

namespace Entities.Entity
{
    public class GamePlayer : Base.Entity
    {
        [SerializeField] private AttackConfig attackConfig;
        [SerializeField] private CanvasSpawner canvasSpawner;

        private PhotonView photonView;
        public PhotonView PhotonView => photonView;
        
        private string playerName;
        public string PlayerName => playerName;
        public void SetupPlayerName(string setName) => playerName = setName;
        
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

        public void InitializeCanvas()
        {
            canvasSpawner.Initialize();
        }
    }
}
