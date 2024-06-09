using System;
using System.Collections.Generic;
using Entities.Entity;
using Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SO
{
    [Serializable]
    public class ZombieSpawnRate
    {
        public GameMode gameMode;
        public float initRate;
        public float respawnRate;
    }
    
    [CreateAssetMenu(menuName = "Game Configs/Zombie Config", fileName = "Zombie Config", order = 2)]
    public class FlyweightZombieSettings : SerializedScriptableObject
    {
        [TableList] [SerializeField] private List<ZombieSpawnRate> zombieSpawnRates;
        public GameObject prefab;
        public float despawnDelay = 5f;
        public int zombieHealth = 100;

        public float GetZombieInitRate(GameMode gameMode)
        {
            return zombieSpawnRates.Find(_ => _.gameMode == gameMode).initRate;
        }

        public float GetZombieRespawnRate(GameMode gameMode)
        {
            return zombieSpawnRates.Find(_ => _.gameMode == gameMode).respawnRate;
        }

        public Zombie Create()
        {
            var zombie = Instantiate(prefab).GetComponent<Zombie>();
            zombie.EnemyHealth = zombieHealth;

            return zombie;
        }

        public void OnGet(Zombie z) => z.gameObject.SetActive(true);
        public void OnRelease(Zombie z) => z.gameObject.SetActive(false);
        public void OnDestroyObject(Zombie z) => Destroy(z.gameObject);
    }
}