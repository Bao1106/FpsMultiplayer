using System.Collections.Generic;
using Interfaces.UI;
using Michsky.UI.Dark;
using Photon.Realtime;
using UnityEngine;

namespace Managers.Multiplayer
{
    public class UiRoomHandle : MonoBehaviour, IRoomManager
    {
        [SerializeField] private Transform contain;
        [SerializeField] private GameObject prefab;
        [SerializeField] private ModalWindowManager modalManager;
        
        public void SetupRoom(List<RoomInfo> lstRoom)
        {
            foreach (var roomInfo in lstRoom)
            {
                var room = Instantiate(prefab, contain).GetComponent<RoomButton>();
                room.GetModalManager(modalManager);
                room.GetRoomInfo(roomInfo);
            }
        }
    }
}