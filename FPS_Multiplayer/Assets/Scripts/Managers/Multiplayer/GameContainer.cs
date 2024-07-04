using System;
using System.Collections.Generic;
using Managers.Multiplayer.Base;
using Services.Utils;
using UnityEngine;

namespace Managers.Multiplayer
{
    public class GameContainer : Singleton<GameContainer>
    {
        public readonly Dictionary<string, bool> RegisterPlayers = new();
        
        protected override void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}
