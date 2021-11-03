using System;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lobby
{
    public class NetworkManagerLobby : NetworkManager
    {
        [SerializeField] private int minPlayers = 2;
        private string menuScene = "Scene_Lobby";

        [Header("Room")] [SerializeField] private NetworkRoomPlayerLobby roomPlayerPrefab;

        public static event Action OnClientConnected;
        public static event Action OnClientDisconnected;
        
        public List<NetworkRoomPlayerLobby> RoomPlayers { get; } = new List<NetworkRoomPlayerLobby>();
        public override void OnStartServer() => spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();
        
        public override void OnStartClient()
        {
            var spawnablePrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs");

            foreach (var prefab in spawnablePrefabs)
            {
                NetworkClient.RegisterPrefab(prefab);
            }
        }
        
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
                return;
            }

            if (SceneManager.GetActiveScene().name != menuScene)
            {
                Debug.Log($"{SceneManager.GetActiveScene().name} != {menuScene} ");
                conn.Disconnect();
            }
        }

        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            if (SceneManager.GetActiveScene().name == menuScene)
            {
                bool isLeader = RoomPlayers.Count == 0;

                NetworkRoomPlayerLobby roomPlayerLobby = Instantiate(roomPlayerPrefab);

                roomPlayerLobby.IsLeader = isLeader;

                NetworkServer.AddPlayerForConnection(conn, roomPlayerLobby.gameObject);
            }
            else
            {
                Debug.Log($"{SceneManager.GetActiveScene().name} != {menuScene} ");
            }
        }

        public override void OnServerDisconnect(NetworkConnection conn)
        {
            if (conn.identity != null)
            {
                var player = conn.identity.GetComponent<NetworkRoomPlayerLobby>();

                RoomPlayers.Remove(player);

                NotifyPlayersOfReadyState();
            }

            base.OnServerDisconnect(conn);
        }

        public void NotifyPlayersOfReadyState()
        {
            foreach(var player in RoomPlayers)
            {
                player.HandleReadyToStart(IsReadyToStart());
            }
        }

        private bool IsReadyToStart()
        {
            if (numPlayers < minPlayers) return false;
            foreach (var player in RoomPlayers)
            {
                if (!player.IsReady) return false;
            }

            return true;
        }

        public void StartGame()
        {
            if (SceneManager.GetActiveScene().name == menuScene)
            {
                if (!IsReadyToStart()) { return; }

                OnServerChangeScene("Scene_Game");
            }
        }

        public override void OnServerSceneChanged(string sceneName)
        {
            if (SceneManager.GetActiveScene().name == menuScene)
            {
                foreach(NetworkRoomPlayerLobby player in RoomPlayers)
                {
                    var gameObject = Instantiate(playerPrefab);
                    var conn = player.connectionToServer;
                    NetworkServer.Destroy(conn.identity.gameObject);

                    NetworkServer.ReplacePlayerForConnection(conn, gameObject.gameObject);
                }
                base.OnServerSceneChanged(sceneName);
            }
        }
        public override void OnStopServer()
        {
            RoomPlayers.Clear();
        }
    }
}