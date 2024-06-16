using System;
using System.Threading.Tasks;
using GOAP.Sensors;
using Interfaces;
using Managers.Multiplayer;
using Managers.Multiplayer.Base;
using Photon.Pun;
using Services.DependencyInjection;
using UnityEngine;

namespace Managers
{
    public class SceneInitManager : MonoBehaviourPunCallbacks, ISceneInit
    {
        private readonly TaskCompletionSource<bool> taskCompletion = new();

        private MultiplayerData playerData;
        private string playerName;
        
        public Task SceneInitTask => taskCompletion.Task;
        public void InitComplete()
        {
            taskCompletion.SetResult(true);
        }

        private void Start()
        {
            playerData = GameConnectManager.Instance.MultiplayerData;
            playerName = playerData.PlayerName;
            Debug.LogError(playerName);
            
            Debug.LogError(PhotonNetwork.InLobby);
            Debug.LogError(PhotonNetwork.InRoom);
            Debug.LogError(PhotonNetwork.IsMasterClient);
        }
        
        
    }
}