using System;
using Enums;
using Photon.Pun;
using Photon.Realtime;
using Services.DependencyInjection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers.Multiplayer.Base
{
    public class MultiplayerData
    {
        public bool IsMasterClient;
        public string PlayerName;
        public string RoomName;
    }
    
    public class BaseConnectRoom : MonoBehaviourPunCallbacks, IConnectRoom
    {
        [SerializeField] private ModalWindowType windowType;
        [SerializeField] private TMP_InputField inputRoomName, inputPlayerName;
        [SerializeField] private Button btnInteract;

        private readonly RoomOptions roomOptions = new() { MaxPlayers = 4 };

        private readonly MultiplayerData multiplayerData = new();
        private string roomName, playerName;

        protected Button BtnInteract => btnInteract;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        protected void OnClickRoomInteract()
        {
            roomName = inputRoomName.text;
            playerName = inputPlayerName.text;
            
            switch (windowType)
            {
                case ModalWindowType.CreateRoom:
                    PhotonNetwork.CreateRoom(roomName, roomOptions);
                    break;
                case ModalWindowType.JoinRoom:
                    PhotonNetwork.JoinRoom(roomName);
                    break;
            }
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
            
            multiplayerData.IsMasterClient = PhotonNetwork.IsMasterClient;
            multiplayerData.PlayerName = playerName;
            multiplayerData.RoomName = roomName;

            GameConnectManager.Instance.MultiplayerData = multiplayerData;
            
            PhotonNetwork.LoadLevel(1);
        }
        
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.LogError($"Room creation failed: {message}");
        }
    }
}