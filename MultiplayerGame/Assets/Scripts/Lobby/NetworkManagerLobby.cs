using System;
using System.Collections.Generic;
using System.Linq;
using Assets.GameLogic;
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

        #region GameLogic
        public List<NetworkRoomPlayerLobby> RoomPlayers { get; } = new List<NetworkRoomPlayerLobby>();

        private List<NetworkGamePlayer> alivePlayers = new List<NetworkGamePlayer>();

        private List<NetworkGamePlayer> deadPlayers = new List<NetworkGamePlayer>();

        private List<NetworkGamePlayer> innocentPlayers = new List<NetworkGamePlayer>();

        private List<NetworkGamePlayer> terroristPlayers = new List<NetworkGamePlayer>();

        public static event Action<NetworkGamePlayer> OnDie;

            
        [Server]
        public void AssignRoles()
        {
            if (alivePlayers.Count != RoomPlayers.Count)
            {
                Debug.LogError("Method AssignRoles() called with an unequal amount of lobby and gameplayers");
                return;
            }

            Debug.Log("Method AssignRoles() invoked. ");
            for (int i = 0; i < alivePlayers.Count; i++)
            {
                if (i == 0)
                {
                    alivePlayers[i].SetRole(Role.Terrorist);
                }
                else
                {
                    alivePlayers[i].SetRole(Role.Innocent);
                }
                alivePlayers[i].ShowRoleText();
            }
        }

        public override bool OnRoomServerSceneLoadedForPlayer(NetworkConnection conn, GameObject roomPlayer, GameObject gamePlayer)
        {
            gamePlayer.GetComponent<NetworkGamePlayer>().name = UnityEngine.Random.Range(1, 100).ToString();
            alivePlayers.Add(gamePlayer.GetComponent<NetworkGamePlayer>());
            alivePlayers.ForEach(x => Debug.Log($"A person has connected to the game: {x.name}"));
            return true;
        }

        public override void OnServerChangeScene(string sceneName)
        {
            if (sceneName == RoomScene)
            {
                foreach (NetworkRoomPlayer roomPlayer in roomSlots)
                {
                    if (roomPlayer == null)
                        continue;

                    // find the game-player object for this connection, and destroy it
                    NetworkIdentity identity = roomPlayer.GetComponent<NetworkIdentity>();

                    if (NetworkServer.active)
                    {
                        // re-add the room object
                        roomPlayer.GetComponent<NetworkRoomPlayer>().readyToBegin = false;
                        NetworkServer.ReplacePlayerForConnection(identity.connectionToClient, roomPlayer.gameObject);
                    }
                }

                allPlayersReady = false;
            }

            base.OnServerChangeScene(sceneName);
        }
        #endregion

        #region LobbyLogic
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
    }
    #endregion
}