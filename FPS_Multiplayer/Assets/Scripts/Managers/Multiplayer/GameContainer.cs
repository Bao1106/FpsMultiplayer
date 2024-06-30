using System.Collections.Generic;
using Managers.Multiplayer.Base;
using Services.Utils;
using UnityEngine;

namespace Managers.Multiplayer
{
    public class GameContainer : Singleton<GameContainer>
    {
        public MultiplayerData MultiplayerData;
        [HideInInspector] public List<string> playersName = new();
        
        protected override void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}
