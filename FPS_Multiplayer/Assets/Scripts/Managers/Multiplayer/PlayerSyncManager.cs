using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers.Multiplayer
{
    public class PlayerSyncManager : MonoBehaviourPunCallbacks
    {
        private readonly string sceneGameplay = "GameplayScene";
        
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
                    Debug.Log($"Synced player: {nickname}");
                    PlayerManager.Instance.OnPlayerJoined(nickname);
                }
            }
        }
        
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            if (newPlayer.CustomProperties.TryGetValue("Nickname", out var nicknameObject))
            {
                var nickname = (string)nicknameObject;
                Debug.Log($"New player joined: {nickname}");
                GameContainer.Instance.playersName.Add(nickname);
                
                //PlayerManager.Instance.OnPlayerJoined(nickname);
            }
        }
    }
}
