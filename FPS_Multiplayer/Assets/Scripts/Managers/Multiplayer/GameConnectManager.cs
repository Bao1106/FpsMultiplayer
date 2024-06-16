using System;
using System.Collections;
using System.Collections.Generic;
using Managers.Multiplayer.Base;
using Photon.Pun;
using Photon.Realtime;
using Services.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers.Multiplayer
{
    public class GameConnectManager : Singleton<GameConnectManager>
    {
        public MultiplayerData MultiplayerData;
        
        protected override void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}
