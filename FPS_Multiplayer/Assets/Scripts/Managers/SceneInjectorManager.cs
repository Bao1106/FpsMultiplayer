using System;
using Enums;
using GOAP.Behaviours;
using Services.DependencyInjection;
using Services.Utils;
using SO;
using UnityEngine;

namespace Managers
{
    public class SceneInjectorManager : Singleton<SceneInjectorManager>
    {
        [SerializeField] private SceneInitManager sceneInitManager;
        [SerializeField] private FlyweightZombieSettings settings;
        
        public SceneInitManager SceneInitManager => sceneInitManager;
        
        private void Start()
        {
            InitSpawnZombie();
            ZombieManager.Instance.Initialize(sceneInitManager);
        }

        private void InitSpawnZombie()
        {
            var rate = settings.GetZombieInitRate(GameMode.Single);
            for (int i = 0; i < rate; i++)
            {
                var zombie = ZombieManager.Spawn(settings);
                zombie.zombieName = $"{i}_{zombie.gameObject.name}";
                ZombieManager.Instance.StoreZombies(zombie);
            }
            ZombieManager.Instance.InitZombiePlayerSensor();
        }
    }
}