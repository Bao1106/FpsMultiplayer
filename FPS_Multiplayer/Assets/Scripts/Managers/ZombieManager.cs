using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Entity;
using Enums;
using ExitGames.Client.Photon;
using GOAP.Sensors;
using Interfaces;
using Photon.Pun;
using Services.DependencyInjection;
using Services.Utils;
using SO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

namespace Managers
{
    public class ZombieManager : Singleton<ZombieManager>
    {
        [SerializeField] private FlyweightZombieSettings setting;
        
        [SerializeField] private bool collectionCheck = true;
        [SerializeField] private int maxCapacity = 100;
        [SerializeField] private int defaultCapacity;

        private readonly Dictionary<GameMode, IObjectPool<Zombie>> pools = new();
        private readonly GameMode gameMode = GameMode.Single;
        private ISceneInit sceneInit;

        public static UnityAction<float> OnGetRespawnRate;
        public Dictionary<string, Zombie> Zombies { get; private set; } = new();

        public void StoreZombies(Zombie zombie)
        {
            Zombies[zombie.ZombieName] = zombie;
        }
        
        public void SyncZombiesForNewPlayer()
        {
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("Zombies", out var zombies))
            {
                Zombies = zombies as Dictionary<string, Zombie> ?? new Dictionary<string, Zombie>();
                foreach (var zombie in Zombies)
                {
                    zombie.Value.OnSyncHealth(zombie.Value.EnemyHealth.Value);
                }
            }
        }
        
        public void UpdateZombieSensors(string playerName)
        {
            foreach (var zombie in Zombies)
            {
                Injector.Instance.RegisterProvider(zombie.Value.GetSensor(), zombie.Key);

                var observer = zombie.Value.GetComponent<ObserverAgentStats>();
                observer.OnInjectListener(zombie.Value, playerName);
            }
        }

        public void OnInjectPlayerSensor(string enemyName, UnityAction<string, bool> callback)
        {
            Zombies[enemyName].PlayerSensor = (IPlayerSensor)Injector.Instance.Resolve(typeof(IPlayerSensor), enemyName);
            Zombies[enemyName].PlayerSensor.UpdatePlayerList(PlayerManager.Instance.Players.Values.ToList());
            Zombies[enemyName].PlayerSensor.IsUserInRange.AddListener(callback);
        }

        public void CheckPool()
        {
            var zombieReleaseCount = Zombies.Values
                .ToList()
                .FindAll(z => !z.gameObject.activeSelf)
                .Count;

            if (zombieReleaseCount >= setting.GetZombieKillRate(gameMode))
            {
                OnGetRespawnRate.Invoke(setting.GetZombieRespawnRate(gameMode));
            }
        }

        public static Zombie Spawn(FlyweightZombieSettings s)
            => Instance.GetPoolFor(s).Get();

        public static void ReturnToPool(Zombie z)
            => Instance.GetPoolFor(Instance.setting)?.Release(z);
        
        private IObjectPool<Zombie> GetPoolFor(FlyweightZombieSettings settings)
        {
            if (pools.TryGetValue(gameMode, out var pool)) 
                return pool;

            pool = new ObjectPool<Zombie>(
                settings.Create,
                settings.OnGet,
                settings.OnRelease,
                settings.OnDestroyObject,
                collectionCheck,
                defaultCapacity,
                maxCapacity);
            pools.Add(gameMode, pool);
            
            return pool;
        }
    }
}
