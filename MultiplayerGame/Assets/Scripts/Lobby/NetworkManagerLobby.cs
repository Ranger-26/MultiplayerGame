﻿using System;
using System.Collections.Generic;
using System.Linq;
using Game;
using Mirror;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lobby
{
    public class NetworkManagerLobby : NetworkRoomManager
    {
        [SerializeField]private string menuScene = "Scene_Menu";
        public static event Action OnClientConnected;
        public static event Action OnClientDisconnected;
        public List<NetworkRoomPlayerLobby> RoomPlayers { get; } = new List<NetworkRoomPlayerLobby>();

        public static event Action<NetworkGamePlayer> OnDie;
        
        //lobby logic
        public override void OnClientConnect(NetworkConnection conn)
        {
            base.OnClientConnect(conn);
            OnClientConnected?.Invoke();
        }

        public override void OnClientDisconnect(NetworkConnection conn)
        {
            base.OnClientDisconnect(conn);

            OnClientDisconnected?.Invoke();
        }
        
        public override void OnServerConnect(NetworkConnection conn)
        {
            if (numPlayers >= maxConnections)
            {
                conn.Disconnect();
            }
            
            if (SceneManager.GetActiveScene().name != menuScene)
            {
                Debug.Log($"{SceneManager.GetActiveScene().name} != {menuScene} ");
                conn.Disconnect();
            }
            
        }
        
        
        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            bool isLeader = RoomPlayers.Count == 0;

            NetworkRoomPlayerLobby roomPlayerLobby = (NetworkRoomPlayerLobby)Instantiate(roomPlayerPrefab);
                
            roomPlayerLobby.IsLeader = isLeader;

            NetworkServer.AddPlayerForConnection(conn, roomPlayerLobby.gameObject);
        }
        
        
        public override void OnServerDisconnect(NetworkConnection conn)
        {
            if (conn.identity != null)
            {
                var player = conn.identity.GetComponent<NetworkRoomPlayerLobby>();

                RoomPlayers.Remove(player);
            }

            base.OnServerDisconnect(conn);
        }

        public override void OnStopServer()
        {
            RoomPlayers.Clear();
        }
        //end lobby logic
        
        //lobby to game logic
        public override bool OnRoomServerSceneLoadedForPlayer(NetworkConnection conn, GameObject roomPlayer, GameObject gamePlayer)
        {
            //_alivePlayers.Add(gamePlayer.GetComponent<NetworkGamePlayer>());
            GameManager.Instance.alivePlayers.Add(gamePlayer.GetComponent<NetworkGamePlayer>());
            return true;
        }

        public override void OnRoomServerSceneChanged(string sceneName)
        {
            Debug.Log(sceneName);
            if (sceneName != GameplayScene) return;
            GameManager.Instance.StartCoroutine(GameManager.Instance.AssignRoles());
        }
        //end lobby to game logic
        
        
        
        
        
        
    }
}