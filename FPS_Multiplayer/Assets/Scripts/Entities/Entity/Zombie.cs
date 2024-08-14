using Entities.Base;
using GOAP.Sensors;
using Managers;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities.Entity
{
    public class Zombie : Enemy, IPunObservable
    {
        private static readonly int deadType = Animator.StringToHash("DeadType");
        private static readonly int dead = Animator.StringToHash("Dead");
        
        public string ZombieName { get; set; }
        public IPlayerSensor PlayerSensor;

        private PhotonView photonView;
        
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
            photonView = GetComponent<PhotonView>();
            
            playerSensor.SetKey(ZombieName);
            base.Awake();
        }

        protected override void Start()
        {
            //EnemyHealth = 100;
            base.Start();
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (!photonView.IsMine) return;
            
            if (other.collider.TryGetComponent(out Bullet bullet))
            {
                var damage = bulletConfig.GetBulletDamage(bullet.GetBullet());
                OnDamaged(damage);
            }
        }

        public override void OnDamaged(int damage)
        {
            base.OnDamaged(damage);
            OnSyncHealth(EnemyHealth.Value);
        }

        public void OnSyncHealth(int health)
        {
            //photonView.RPC("RpcUpdateHealth", RpcTarget.All, health);
        }
        
        [PunRPC]
        private void RpcUpdateHealth(int health)
        {
            if (!photonView.IsMine)
            {
                EnemyHealth.Value = health;
            }
        }
        
        public void ReturnToPool()
        {
            //Destroy(gameObject);
            ZombieManager.ReturnToPool(this);
            ZombieManager.Instance.CheckPool();
        }
        
        public PlayerSensor GetSensor() => playerSensor;
        
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(EnemyHealth);
            }
            else
            {
                EnemyHealth.Value = (int)stream.ReceiveNext();
            }
        }
    }
}