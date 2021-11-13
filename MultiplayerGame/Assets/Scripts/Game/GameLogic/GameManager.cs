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
        public SyncList<NetworkGamePlayer> alivePlayers = new SyncList<NetworkGamePlayer>();
        
        public SyncList<NetworkGamePlayer> deadPlayers = new SyncList<NetworkGamePlayer>();

        public SyncList<NetworkGamePlayer> innocentPlayers = new SyncList<NetworkGamePlayer>();

        public SyncList<NetworkGamePlayer> terroristPlayers = new SyncList<NetworkGamePlayer>();

        public static GameManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        public override void OnStartServer()
        {
            alivePlayers.Callback += OnAlivePlayersUpdated;
        }

        private void OnAlivePlayersUpdated(SyncList<NetworkGamePlayer>.Operation op, int index, NetworkGamePlayer oldItem, NetworkGamePlayer newItem)
        {
            Debug.Log($"Player {newItem.name} has joined!");
        }

        [Command]
        public void AddPlayer(NetworkGamePlayer player) => alivePlayers.Add(player);
    }
}