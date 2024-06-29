using System.Collections.Generic;
using System.Linq;
using Entities.Entity;
using Events;
using ExitGames.Client.Photon;
using Managers.Multiplayer;
using Managers.Multiplayer.Base;
using Photon.Pun;
using Photon.Realtime;
using Services.DependencyInjection;
using Services.Utils;
using UnityEngine;
using EventCode = Enums.EventCode;

namespace Managers
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        [SerializeField] private GameObject prefabPlayer, spawnPoint;
        
        private Vector3 GetSpawnPointSize() => spawnPoint.GetComponent<Renderer>().bounds.size;
        private MultiplayerData playerData;
        private readonly Dictionary<string, GamePlayer> players = new ();
        
        public IReadOnlyDictionary<string, GamePlayer> Players => players;
        public MultiplayerData PlayerData => playerData;
        
        protected override void Awake()
        {
            playerData = GameContainer.Instance.MultiplayerData;
        }
        
        private Vector3 CalculateRandomSpawnPosition()
        {
            var size = GetSpawnPointSize();
            var randomX = Random.Range(-size.x / 2, size.x / 2);
            var randomZ = Random.Range(-size.z / 2, size.z / 2);
            return new Vector3(randomX, prefabPlayer.transform.position.y, randomZ);
        } 
        
        public void InitPlayer(SceneInitManager initManager)
        {
            var randomPos = CalculateRandomSpawnPosition();

            var player = PhotonNetwork
                .Instantiate(prefabPlayer.name, randomPos, Quaternion.identity)
                .GetComponent<GamePlayer>();
            players[PlayerData.PlayerName] = player;

            if (playerData.IsMasterClient)
            {
                StaticEvents.SpawnPlayerCompleted.SetResult(true);
                ZombieManager.Instance.Initialize(initManager);
            }
            
            Injector.Instance.RegisterProvider(player, PlayerData.PlayerName);
            PhotonNetwork.RaiseEvent((byte)EventCode.PlayerSpawned, PlayerData.PlayerName, RaiseEventOptions.Default, SendOptions.SendReliable);
        }
        
        public void OnPlayerJoined(string playerName)
        {
            if (!players.ContainsKey(playerName))
            {
                var player = FindObjectsOfType<GamePlayer>().FirstOrDefault(p => p.PhotonView.Owner.NickName == playerName);
                if (player != null)
                {
                    players[playerName] = player;
                    Injector.Instance.RegisterProvider(player, playerName);
                    ZombieManager.Instance.UpdateZombieSensors();
                }
            }
        }
    }
}
