using Managers.Multiplayer.Base;
using Services.Utils;

namespace Managers.Multiplayer
{
    public class GameContainer : Singleton<GameContainer>
    {
        public MultiplayerData MultiplayerData;
        
        protected override void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}
