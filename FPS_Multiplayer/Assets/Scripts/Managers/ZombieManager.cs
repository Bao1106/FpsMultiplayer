using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Entity;
using GOAP.Sensors;
using Interfaces;
using Services.DependencyInjection;
using Services.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class ZombieManager : Singleton<ZombieManager>
    {
        [SerializeField] private List<Zombie> lstZombie;
        
        private readonly Dictionary<string, IPlayerSensor> playerSensors = new ();
        private readonly TaskCompletionSource<bool> taskCompletion = new ();
        private ISceneInit sceneInit;
        
        public void Initialize(ISceneInit manager)
        {
            sceneInit = manager;
            taskCompletion.SetResult(true);
        }

        public async void InitZombiePlayerSensor()
        {
            await taskCompletion.Task;
            foreach (var zombie in lstZombie)
            {
                Injector.Instance.RegisterProvider(zombie.GetSensor(), zombie.zombieName);
                //Injector.Instance.InjectSingleField(this, typeof(IPlayerSensor), zombie.zombieName);
                //zombie.PlayerSensor = (IPlayerSensor)Injector.Instance.Resolve(typeof(IPlayerSensor), zombie.EnemyName);
                //zombie.PlayerSensor.IsUserInRange.AddListener(OnDemoUpdate);
                
                StorePlayerSensor(zombie.zombieName, zombie.GetSensor());
            }
            
            sceneInit.InitComplete();
        }

        public void OnInjectPlayerSensor(string enemyName, UnityAction<string, bool> callback)
        {
            var id = lstZombie.FindIndex(_ => _.zombieName.Equals(enemyName));
            lstZombie[id].PlayerSensor = (IPlayerSensor)Injector.Instance.Resolve(typeof(IPlayerSensor), enemyName);
            lstZombie[id].PlayerSensor.IsUserInRange.AddListener(callback);
        }
        
        private void OnDemoUpdate(string testName, bool test)
        {
            Debug.LogError($"Check name: {testName}");
            Debug.LogError($"Check bool: {test}");
        }
        
        private void StorePlayerSensor(string key, IPlayerSensor playerSensor)
        {
            playerSensors.TryAdd(key, playerSensor);
        }
    }
}
