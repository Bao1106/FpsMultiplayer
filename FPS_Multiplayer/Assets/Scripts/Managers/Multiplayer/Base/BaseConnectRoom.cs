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
        
        protected readonly MultiplayerData MultiplayerData = new();
        protected string RoomName, PlayerName;

        protected Button BtnInteract => btnInteract;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        protected void OnClickRoomInteract()
        {
            RoomName = inputRoomName.text;
            PlayerName = inputPlayerName.text;
            
            switch (windowType)
            {
                case ModalWindowType.CreateRoom:
                    PhotonNetwork.CreateRoom(RoomName, roomOptions);
                    break;
                case ModalWindowType.JoinRoom:
                    PhotonNetwork.JoinRoom(RoomName);
                    break;
            }
        }
    }
}