using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

namespace Interfaces.UI
{
    public interface IRoomManager
    {
        void SetupRoom(List<RoomInfo> lstRoom);
    }
}