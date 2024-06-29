using Unity.VisualScripting;

namespace Enums
{
    public enum GameMode 
    {
        Single = 0,
        Multiplayer = 1
    }
        
    public enum BulletType
    {
        CasingBig = 0,
        CasingSmall = 1,
        CasingGrenade = 2,
        CasingShell = 3
    }

    public enum ModalWindowType
    {
        Exit = 0,
        LoadChapter = 1,
        LoadSave = 2,
        BindKey = 3,
        ConnectServer = 4,
        MultiplayerFilter = 5,
        CreateRoom = 6,
        JoinRoom = 7
    }
    
    public enum EventCode : byte
    {
        PlayerSpawned = 1,
    }
}