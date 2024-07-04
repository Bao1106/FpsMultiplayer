using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers.Multiplayer
{
    public class PlayerSyncManager : MonoBehaviourPunCallbacks
    {
        private readonly string sceneGameplay = "GameplayScene";
        private readonly string registerPlayersKey = "RegisterPlayers";
        
        public override void OnEnable()
        {
            base.OnEnable();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name.Equals(sceneGameplay)) // Giả sử scene 2 có buildIndex là 1
            {
                OnSyncPlayers();
            }
        }
        
        private void OnSyncPlayers()
        {
            foreach (var player in PhotonNetwork.PlayerList)
            {
                if (player.CustomProperties.TryGetValue("Nickname", out var nicknameObject))
                {
                    var nickname = (string)nicknameObject;

                    if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("RegisterPlayers", out var players))
                    {
                        GameContainer.Instance.RegisterPlayers = 
                            players as Dictionary<string, bool> ?? new Dictionary<string, bool>();
                        
                        if (GameContainer.Instance.RegisterPlayers.ContainsKey(nickname)) continue;
                        
                        Debug.Log($"Synced player: {nickname}");
                    
                        PlayerManager.Instance.InitPlayer(nickname);
                        PlayerManager.Instance.OnPlayerJoined(nickname);

                        GameContainer.Instance.RegisterPlayers.TryAdd(nickname, true);
                        
                        UpdatePlayersRegister();
                    
                        if(!PhotonNetwork.IsMasterClient) SceneInjectorManager.Instance.OnInject();
                    }
                }
            }
        }
        
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            if (newPlayer.CustomProperties.TryGetValue("Nickname", out var nicknameObject))
            {
                var nickname = (string)nicknameObject;
                Debug.Log($"New player joined: {nickname}");
                
                //PlayerManager.Instance.OnPlayerJoined(nickname);
            }
        }

        private void UpdatePlayersRegister()
        {
            var roomProperties = new Hashtable
            {
                ["RegisterPlayers"] = GameContainer.Instance.RegisterPlayers
            };
            
            PhotonNetwork.CurrentRoom.SetCustomProperties(roomProperties);
        }
    }
}
