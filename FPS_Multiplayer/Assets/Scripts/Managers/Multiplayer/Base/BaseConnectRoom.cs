using Enums;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
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

        protected void OnClickRoomInteract()
        {
            roomName = inputRoomName.text;
            playerName = inputPlayerName.text;

            PhotonNetwork.NickName = playerName;
            
            switch (windowType)
            {
                case ModalWindowType.CreateRoom:
                    PhotonNetwork.CreateRoom(roomName, roomOptions);
                    break;
                case ModalWindowType.JoinRoom:
                    PhotonNetwork.JoinRoom(roomName);
                    break;
            }
            
            multiplayerData.IsMasterClient = PhotonNetwork.IsMasterClient;
            multiplayerData.PlayerName = playerName;
            multiplayerData.RoomName = roomName;

            GameContainer.Instance.MultiplayerData = multiplayerData;
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

            var properties = new Hashtable
            {
                ["Nickname"] = PhotonNetwork.NickName
            };

            PhotonNetwork.LocalPlayer.SetCustomProperties(properties);
            PhotonNetwork.LoadLevel(1);
        }
        
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.LogError($"Room creation failed: {message}");
        }
    }
}