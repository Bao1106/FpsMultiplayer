using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Entity;
using Enums;
using GOAP.Sensors;
using Interfaces;
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
        [SerializeField] private List<Zombie> lstZombie;
        [SerializeField] private FlyweightZombieSettings setting;
        
        [SerializeField] private bool collectionCheck = true;
        [SerializeField] private int maxCapacity = 100;
        [SerializeField] private int defaultCapacity;

        private readonly Dictionary<GameMode, IObjectPool<Zombie>> pools = new();
        private readonly TaskCompletionSource<bool> taskCompletion = new ();
        private readonly GameMode gameMode = GameMode.Single;
        private ISceneInit sceneInit;

        public static UnityAction<float> OnGetRespawnRate;
        
        public void Initialize(ISceneInit manager)
        {
            sceneInit = manager;
            taskCompletion.SetResult(true);
        }

        public void StoreZombies(Zombie zombie)
        {
            lstZombie.Add(zombie);
        }
        
        public async void UpdateZombieSensors()
        {
            await taskCompletion.Task;
            foreach (var zombie in lstZombie)
            {
                Injector.Instance.RegisterProvider(zombie.GetSensor(), zombie.zombieName);
            }
            
            sceneInit.InitComplete();
        }

        public void OnInjectPlayerSensor(string enemyName, UnityAction<string, bool> callback)
        {
            var id = lstZombie.FindIndex(_ => _.zombieName.Equals(enemyName));
            lstZombie[id].PlayerSensor = (IPlayerSensor)Injector.Instance.Resolve(typeof(IPlayerSensor), enemyName);
            lstZombie[id].PlayerSensor.UpdatePlayerList(PlayerManager.Instance.Players.Values.ToList());
            lstZombie[id].PlayerSensor.IsUserInRange.AddListener(callback);
        }

        public void CheckPool()
        {
            var zombieReleaseCount = lstZombie
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
