using System;
using Enums;
using Michsky.UI.Dark;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers.Multiplayer
{
    public class RoomButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text txtRoomName, txtMapName, txtModeGame, txtRoomSlot;

        private Button btnRoom;
        private ModalWindowManager modalWindowManager;
        private ModalConnectServer modalConnectServer;

        private string roomName, mapName, mode;
        private int currentSlot, roomSlot;
        
        public void GetModalManager(ModalWindowManager manager)
        {
            modalWindowManager = manager;
            modalConnectServer = manager.GetComponent<ModalConnectServer>();
        }
        
        private void Start()
        {
            btnRoom = GetComponent<Button>();
            btnRoom.onClick.AddListener(OnClickButton);
        }

        private void OnClickButton()
        {
            BlurManager.Instance.BlurInAnim();
            modalWindowManager.ModalWindowIn();
            modalConnectServer.SetupModal(roomName, mapName, currentSlot, roomSlot);
        }
        
        private void SetupRoomButton()
        {
            txtRoomName.text = roomName;
            txtMapName.text = mapName;
            txtModeGame.text = mode;
            txtRoomSlot.text = $"Slot: {currentSlot}/{roomSlot}";
        }
        
        public void GetRoomInfo(RoomInfo info)
        {
            roomName = info.Name;
            mapName = "Demo Map";
            mode = "Demo Match";
            currentSlot = info.PlayerCount;
            roomSlot = info.MaxPlayers;
            
            SetupRoomButton();
        }
    }
}