using System;
using System.Collections.Generic;
using Enums;
using Interfaces.UI;
using Michsky.UI.Dark;
using Photon.Realtime;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Managers.Multiplayer
{
    public class UiRoomHandle : SerializedMonoBehaviour, IRoomManager
    {
        [TableList] [SerializeField] private List<ModalWindows> modalWindows;
        [SerializeField] private Transform contain;
        [SerializeField] private GameObject prefab;
        [SerializeField] private Button btnCreateRoom, btnJoinRoom;

        private void Start()
        {
            btnCreateRoom.onClick.AddListener(() => OnClickInitRoom(ModalWindowType.CreateRoom));
            btnJoinRoom.onClick.AddListener(() => OnClickInitRoom(ModalWindowType.JoinRoom));
        }

        private ModalWindowManager GetModalManager(ModalWindowType type)
        {
            return modalWindows.Find(modal => modal.type == type)?.modalManager;
        }

        private void OnClickInitRoom(ModalWindowType type)
        {
            BlurManager.Instance.BlurInAnim();
            
            var modal = GetModalManager(type);
            modal.ModalWindowIn();
        }
        
        public void SetupRoom(List<RoomInfo> lstRoom)
        {
            foreach (var roomInfo in lstRoom)
            {
                var room = Instantiate(prefab, contain).GetComponent<RoomButton>();
                room.GetModalManager(GetModalManager(ModalWindowType.ConnectServer));
                room.GetRoomInfo(roomInfo);
            }
        }
    }
}