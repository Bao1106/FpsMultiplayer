using Enums;
using Managers.Multiplayer.Base;
using Photon.Pun;
using Services.DependencyInjection;
using UnityEngine;

namespace Managers.Multiplayer
{
    public class CreateRoom : BaseConnectRoom
    {
        private void Start()
        {
            BtnInteract.onClick.AddListener(OnClickRoomInteract);
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();
            Debug.Log("Room created successfully.");
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            Debug.Log("Joined room successfully.");

            MultiplayerData.IsMasterClient = PhotonNetwork.IsMasterClient;
            MultiplayerData.PlayerName = PlayerName;
            MultiplayerData.RoomName = RoomName;

            GameConnectManager.Instance.MultiplayerData = MultiplayerData;
            
            PhotonNetwork.LoadLevel(1);
        }
        
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.LogError($"Room creation failed: {message}");
        }
    }
}