using System.Threading.Tasks;
using Events;
using Interfaces;
using Managers.Multiplayer;
using Managers.Multiplayer.Base;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Managers
{
    public class SceneInitManager : MonoBehaviourPunCallbacks, ISceneInit
    {
        [SerializeField] private GameObject prefabPlayer, spawnPoint;
        
        private readonly TaskCompletionSource<bool> taskCompletion = new();

        private MultiplayerData playerData;
        private string playerName;
        
        private Vector3 GetSpawnPointSize() => spawnPoint.GetComponent<Renderer>().bounds.size;
        
        public Task SceneInitTask => taskCompletion.Task;
        public void InitComplete()
        {
            taskCompletion.SetResult(true);
        }

        private void Awake()
        {
            playerData = GameConnectManager.Instance.MultiplayerData;
            playerName = playerData.PlayerName;
            
            InitPlayer();
        }

        private void InitPlayer()
        {
            var size = GetSpawnPointSize();
            var randomX = Random.Range(-size.x / 2, size.x / 2);
            var randomZ = Random.Range(-size.z / 2, size.z / 2);
            var randomPos = new Vector3(randomX, prefabPlayer.transform.position.y, randomZ);

            var player = PhotonNetwork
                .Instantiate(prefabPlayer.name, randomPos, Quaternion.identity);
            
            StaticEvents.SpawnPlayerCompleted.SetResult(true);
            ZombieManager.Instance.Initialize(this);
        }
    }
}