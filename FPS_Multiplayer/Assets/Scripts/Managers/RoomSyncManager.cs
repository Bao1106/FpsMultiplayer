using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Entity;
using ExitGames.Client.Photon;
using Interfaces;
using Photon.Pun;
using UnityEngine;

namespace Managers
{
    public class RoomSyncManager : MonoBehaviourPunCallbacks
    {
        public void OnRegisterZombies(Dictionary<string, Zombie> zombies)
        {
            var roomProperties = new Hashtable
            {
                ["Zombies"] = zombies
            };
            
            PhotonNetwork.CurrentRoom.SetCustomProperties(roomProperties);
        }
    }
}