using Enums;
using Managers.Multiplayer.Base;
using Photon.Pun;
using Services.DependencyInjection;
using UnityEngine;

namespace Managers.Multiplayer
{
    public class JoinRoom : BaseConnectRoom
    {
        private void Start()
        {
            BtnInteract.onClick.AddListener(OnClickRoomInteract);
        }
    }
}