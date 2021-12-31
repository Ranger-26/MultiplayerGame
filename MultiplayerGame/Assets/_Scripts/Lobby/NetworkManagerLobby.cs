using System;
using System.Collections.Generic;
using System.Linq;
using Game;
using Game.GameLogic;
using Game.Player;
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

        public GameObject deadPlayerPrefab;
        
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
            GameManager.instance.alivePlayers.Add(gamePlayer.GetComponent<NetworkGamePlayer>());
            return true;
        }

        public override void OnRoomServerSceneChanged(string sceneName)
        {
            Debug.Log(sceneName);
            if (sceneName != GameplayScene) return;
            GameManager.instance.StartCoroutine(GameManager.instance.AssignRoles());
        }
        //end lobby to game logic
        
        
        
        
        
        
    }
}