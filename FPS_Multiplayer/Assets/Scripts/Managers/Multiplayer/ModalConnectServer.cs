using TMPro;
using UnityEngine;

namespace Managers.Multiplayer
{
    public class ModalConnectServer : MonoBehaviour
    {
        [SerializeField] private TMP_Text txtRoomName, txtMapName, txtSlot;

        public void SetupModal(string roomName, string mapName, int currentSlot, int maxSlot)
        {
            txtRoomName.text = roomName;
            txtMapName.text = mapName;
            txtSlot.text = $"{currentSlot}/{maxSlot}";
        }
    }
}