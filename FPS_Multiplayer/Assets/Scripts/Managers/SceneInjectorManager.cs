using System;
using Enums;
using GOAP.Behaviours;
using Managers.Multiplayer;
using Photon.Pun;
using Services.DependencyInjection;
using Services.Utils;
using SO;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class SceneInjectorManager : Singleton<SceneInjectorManager>
    {
        [SerializeField] private SceneInitManager sceneInitManager;
        [SerializeField] private FlyweightZombieSettings settings;
        
        public SceneInitManager SceneInitManager => sceneInitManager;
        
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
                zombie.zombieName = $"{i}_{zombie.gameObject.name}";
                ZombieManager.Instance.StoreZombies(zombie);
            }

            OnInject();
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