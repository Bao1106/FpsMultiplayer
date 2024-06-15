using System;
using Managers.Multiplayer.Base;

namespace Managers.Multiplayer
{
    public class CreateRoom : BaseConnectRoom
    {
        private void Start()
        {
            BtnInteract.onClick.AddListener(OnClickRoomInteract);
        }
    }
}