using System.Collections.Generic;
using Entities.Entity;
using GOAP.Sensors;
using Services.DependencyInjection;
using Services.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class ZombieManager : Singleton<ZombieManager>
    {
        [SerializeField] private List<Zombie> lstZombie;
        
        private Dictionary<string, IPlayerSensor> playerSensors = new ();

        public void InitZombiePlayerSensor()
        {
            foreach (var zombie in lstZombie)
            {
                Injector.Instance.RegisterProvider(zombie.GetSensor(), zombie.zombieName);
                Injector.Instance.InjectSingleField(this, typeof(IPlayerSensor), zombie.zombieName);
                
                zombie.iPlayerSensor = (IPlayerSensor)Injector.Instance.Resolve(typeof(IPlayerSensor), zombie.zombieName);
                zombie.iPlayerSensor.IsUserInRange.AddListener(OnDemoUpdate);
                
                StorePlayerSensor(zombie.zombieName, zombie.GetSensor());
            }
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
