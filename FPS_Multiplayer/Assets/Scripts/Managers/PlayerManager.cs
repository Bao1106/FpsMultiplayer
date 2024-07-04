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
        private readonly Dictionary<string, GamePlayer> players = new ();
        private GamePlayer player;
        
        public IReadOnlyDictionary<string, GamePlayer> Players => players;
        
        private Vector3 CalculateRandomSpawnPosition()
        {
            var size = GetSpawnPointSize();
            var randomX = Random.Range(-size.x / 2, size.x / 2);
            var randomZ = Random.Range(-size.z / 2, size.z / 2);
            return new Vector3(randomX, prefabPlayer.transform.position.y, randomZ);
        } 
        
        public void InitPlayer(string playerName)
        {
            var randomPos = CalculateRandomSpawnPosition();

            player = PhotonNetwork
                .Instantiate(prefabPlayer.name, randomPos, Quaternion.identity)
                .GetComponent<GamePlayer>();
            player.SetupPlayerName(playerName);

            if (PhotonNetwork.IsMasterClient)
            {
                //StaticEvents.SpawnPlayerCompleted.SetResult(true);
                Injector.Instance.InitializeProvider();
                Injector.Instance.InitializeInjector();
            }
            
            /*if (playerData.IsMasterClient)
            {
                StaticEvents.SpawnPlayerCompleted.SetResult(true);
                //ZombieManager.Instance.Initialize(initManager);
            }*/
        }
        
        public void OnPlayerJoined(string playerName)
        {
            if (!players.ContainsKey(playerName))
            {
                /*player = GetComponents<GamePlayer>().FirstOrDefault(p => p.PhotonView.Owner.NickName == playerName);
                player = GetComponent<PhotonView>()*/
                if (player != null && player.PhotonView.Owner.NickName.Equals(playerName))
                {
                    Injector.Instance.RegisterProvider(player, playerName);

                    player.InitializeCanvas();
                    players[playerName] = player;
                    PhotonNetwork.RaiseEvent((byte)EventCode.PlayerSpawned, playerName, RaiseEventOptions.Default, SendOptions.SendReliable);
                }
            }
        }
    }
}
