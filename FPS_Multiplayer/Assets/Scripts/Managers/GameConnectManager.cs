using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Managers
{
    public class GameConnectManager : MonoBehaviourPunCallbacks
    {
        /*public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            
            if (newPlayer.CustomProperties.TryGetValue("Nickname", out var playerName))
            {
                var nickname = (string)playerName;
                Debug.Log($"New player joined: {nickname}");
                //PlayerManager.Instance.OnPlayerJoined(nickname);
            }
        }*/
    }
}
