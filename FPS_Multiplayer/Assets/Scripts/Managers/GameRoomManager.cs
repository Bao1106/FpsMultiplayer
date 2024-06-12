using Photon.Pun;
using UnityEngine;

namespace Managers
{
    public class GameRoomManager : MonoBehaviourPunCallbacks
    {
        public GameObject player;

        [Space] public Transform spawnPoint;
        
        private void Start()
        {
            Debug.Log("Connecting ........");

            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            Debug.Log("Connected to Server !!");

            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();

            PhotonNetwork.JoinOrCreateRoom("test", null, null);

            Debug.Log("Joined room !!!");
        }
    }
}
