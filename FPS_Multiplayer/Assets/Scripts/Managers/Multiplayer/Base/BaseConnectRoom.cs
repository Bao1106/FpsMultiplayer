using System.Collections;
using Enums;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Managers.Multiplayer.Base
{
    public class BaseConnectRoom : MonoBehaviourPunCallbacks, IConnectRoom
    {
        [SerializeField] private ModalWindowType windowType;
        [SerializeField] private TMP_InputField inputRoomName, inputPlayerName;
        [SerializeField] private Button btnInteract;

        private readonly RoomOptions roomOptions = new() { MaxPlayers = 4 };
        
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
            
            var playerProperties = new Hashtable
            {
                ["Nickname"] = PhotonNetwork.NickName
            };
            PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
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
            
            var syncManagerObj = new GameObject("PlayerSyncManager");
            syncManagerObj.AddComponent<PlayerSyncManager>();
            DontDestroyOnLoad(syncManagerObj);
            
            if (PhotonNetwork.IsMasterClient)
            {
                var roomProperties = new Hashtable
                {
                    ["RegisterPlayers"] = GameContainer.Instance.RegisterPlayers
                };
                PhotonNetwork.CurrentRoom.SetCustomProperties(roomProperties);
            }

            StartCoroutine(LoadRoomLevel());
        }
        
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            //Debug.LogError($"Room creation failed: {message}");
        }

        private IEnumerator LoadRoomLevel()
        {
            yield return new WaitForSeconds(0.1f);
            PhotonNetwork.LoadLevel(1);
        } 
    }
}