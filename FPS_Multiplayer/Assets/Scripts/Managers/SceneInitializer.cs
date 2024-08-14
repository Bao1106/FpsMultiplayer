using Enums;
using Managers.Multiplayer;
using Photon.Pun;
using Services.Utils;
using SO;
using UnityEngine;

namespace Managers
{
    public class SceneInitializer : Singleton<SceneInitializer>
    {
        [SerializeField] private RoomSyncManager roomSyncManager;
        [SerializeField] private FlyweightZombieSettings settings;
        
        private void OnEnable()
        {
            ZombieManager.OnGetRespawnRate += OnRespawn;
        }

        private void OnDisable()
        {
            ZombieManager.OnGetRespawnRate -= OnRespawn;
        }

        private void Start()
        {
            if(PhotonNetwork.IsMasterClient)
                SpawnZombie();
        }

    #region Zombie
        private void SpawnZombie()
        {
            settings.GetPlaneSize();
            var rate = settings.GetZombieInitRate(GameMode.Single);
            
            for (int i = 0; i < rate; i++)
            {
                var zombie = ZombieManager.Spawn(settings);
                zombie.ZombieName = $"{i}_{zombie.gameObject.name}";
                ZombieManager.Instance.StoreZombies(zombie);
            }

            OnInject();
            roomSyncManager.OnRegisterZombies(ZombieManager.Instance.Zombies);
        }
        
        private void OnRespawn(float respawnRate)
        {
            for (int i = 0; i < respawnRate; i++)
            {
                var zombie = ZombieManager.Spawn(settings);
                zombie.EnemyHealth.Value = settings.zombieHealth;
            }
        }
    #endregion

        public void OnInject()
        {
            foreach (var player in GameContainer.Instance.RegisterPlayers)
            {
                ZombieManager.Instance.UpdateZombieSensors(player.Key);
            }
        } 
    }
}