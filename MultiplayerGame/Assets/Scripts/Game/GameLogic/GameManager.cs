using System;
using System.Collections.Generic;
using Mirror;
using Player;
using UnityEditor;
using UnityEngine;

namespace Game
{
    public class GameManager : NetworkBehaviour
    {
        public List<NetworkConnectionToClient> alivePlayers = new List<NetworkConnectionToClient>();
        
        public List<NetworkConnectionToClient> deadPlayers = new List<NetworkConnectionToClient>();

        public List<NetworkConnectionToClient> innocentPlayers = new List<NetworkConnectionToClient>();

        public List<NetworkConnectionToClient> terroristPlayers = new List<NetworkConnectionToClient>();

        public static GameManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        private void OnAlivePlayersUpdated(SyncList<NetworkConnection>.Operation op, int index, NetworkConnection oldItem, NetworkConnection newItem)
        {
            Debug.Log($"Player {newItem.identity.GetComponent<NetworkGamePlayer>().Name} has joined!");
        }

        [Command]
        public void AddPlayer(NetworkConnectionToClient player = null) => alivePlayers.Add(player);
    }
}