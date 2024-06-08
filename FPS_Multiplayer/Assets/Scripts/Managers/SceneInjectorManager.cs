using System;
using GOAP.Behaviours;
using Services.DependencyInjection;
using Services.Utils;
using UnityEngine;

namespace Managers
{
    public class SceneInjectorManager : Singleton<SceneInjectorManager>
    {
        [SerializeField] private SceneInitManager sceneInitManager;

        public SceneInitManager SceneInitManager => sceneInitManager;
        
        private void Start()
        {
            ZombieManager.Instance.InitZombiePlayerSensor();
            ZombieManager.Instance.Initialize(sceneInitManager);
        }
    }
}