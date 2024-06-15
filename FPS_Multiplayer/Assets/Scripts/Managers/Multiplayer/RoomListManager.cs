using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Managers.Multiplayer
{
    public class RoomListManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private UiRoomHandle uiRoomHandle;
        
        private List<RoomInfo> cachedRoom = new();
        
        IEnumerator Start()
        {
            if (PhotonNetwork.InRoom)
            {
                PhotonNetwork.LeaveRoom();
                PhotonNetwork.Disconnect();
            }

            yield return new WaitUntil(() => !PhotonNetwork.IsConnected);
            
            Debug.Log("Connecting ........");

            PhotonNetwork.ConnectUsingSettings();
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();

            PhotonNetwork.JoinLobby();
        }
        
        public  override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            if (cachedRoom.Count <= 0)
            {
                cachedRoom = roomList;
            }
            else
            {
                foreach (var info in roomList)
                {
                    
                }
            } 
            
            uiRoomHandle.SetupRoom(cachedRoom);
        }
    }
}