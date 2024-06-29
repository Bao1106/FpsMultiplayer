using System;
using System.Collections.Generic;
using Entities.Entity;
using Enums;
using Services;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;
using Photon.Pun;

namespace SO
{
    [Serializable]
    public class ZombieSpawnRate
    {
        public GameMode gameMode;
        public float initRate;
        public float respawnRate;
        public float killRate;
    }
    
    [CreateAssetMenu(menuName = "Game Configs/Zombie Config", fileName = "Zombie Config", order = 2)]
    public class FlyweightZombieSettings : SerializedScriptableObject
    {
        [TableList] [SerializeField] private List<ZombieSpawnRate> zombieSpawnRates;
        public GameObject prefab, plane;
        public float despawnDelay = 5f;
        public int zombieHealth = 100;

        private Vector3 planeSize;
        
        public float GetZombieInitRate(GameMode gameMode)
        {
            return zombieSpawnRates.Find(_ => _.gameMode == gameMode).initRate;
        }

        public float GetZombieRespawnRate(GameMode gameMode)
        {
            return zombieSpawnRates.Find(_ => _.gameMode == gameMode).respawnRate;
        }
        
        public float GetZombieKillRate(GameMode gameMode)
        {
            return zombieSpawnRates.Find(_ => _.gameMode == gameMode).killRate;
        }

        public void GetPlaneSize() => planeSize = plane.GetComponent<Renderer>().bounds.size;
        
        public Zombie Create()
        {
            var randomX = Random.Range(-planeSize.x / 2, planeSize.x / 2);
            var randomZ = Random.Range(-planeSize.z / 2, planeSize.z / 2);
            var randomPos = new Vector3(randomX, prefab.transform.position.y, randomZ);
            
            var zombie = PhotonNetwork.Instantiate(prefab.name, randomPos, Quaternion.identity).GetComponent<Zombie>();
            zombie.EnemyHealth = new Observer<int>(zombieHealth);
            zombie.EnemyHealth.Invoke();

            return zombie;
        }

        public void OnGet(Zombie z) => z.gameObject.SetActive(true);
        public void OnRelease(Zombie z) => z.gameObject.SetActive(false);
        public void OnDestroyObject(Zombie z) => Destroy(z.gameObject);
    }
}