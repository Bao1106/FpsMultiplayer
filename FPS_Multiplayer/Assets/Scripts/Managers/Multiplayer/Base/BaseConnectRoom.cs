using Enums;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Managers.Multiplayer.Base
{
    public class BaseConnectRoom : MonoBehaviourPunCallbacks, IConnectRoom
    {
        [SerializeField] private ModalWindowType windowType;
        [SerializeField] private TMP_InputField inputRoomName, inputPlayerName;
        [SerializeField] private Button btnInteract;

        private readonly RoomOptions roomOptions = new() { MaxPlayers = 4 };

        protected Button BtnInteract => btnInteract;
        
        protected void OnClickRoomInteract()
        {
            switch (windowType)
            {
                case ModalWindowType.CreateRoom:
                    PhotonNetwork.CreateRoom(inputRoomName.text, roomOptions);
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
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.LogError($"Room creation failed: {message}");
        }
    }
}