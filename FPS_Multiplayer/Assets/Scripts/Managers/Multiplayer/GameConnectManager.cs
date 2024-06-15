using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace Managers.Multiplayer
{
    public class GameConnectManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_Text txtConnect;
        
        private void Start()
        {
            Debug.Log("Connecting ........");

            PhotonNetwork.ConnectUsingSettings();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            Debug.Log("Connected to Server !!");

            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();

            Debug.Log("Joined lobby !!!");
            
            var testRoomOption = new RoomOptions
            {
                MaxPlayers = 4,
            };
            PhotonNetwork.JoinOrCreateRoom("test", testRoomOption, null);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();

            txtConnect.text = "Room created successfully.";
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.Log($"Room creation failed: {message}");
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            Debug.Log("Joined room successfully.");
        }
    }
}
