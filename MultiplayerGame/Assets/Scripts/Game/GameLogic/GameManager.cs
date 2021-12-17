using System;
using System.Collections;
using System.Collections.Generic;
using Game.Player;
using Lobby;
using Mirror;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace Game.GameLogic
{
    public class GameManager : NetworkBehaviour
    {
        public List<NetworkGamePlayer> alivePlayers = new List<NetworkGamePlayer>();
        
        public List<NetworkGamePlayer> deadPlayers = new List<NetworkGamePlayer>();

        public List<NetworkGamePlayer> innocentPlayers = new List<NetworkGamePlayer>();

        public List<NetworkGamePlayer> terroristPlayers = new List<NetworkGamePlayer>();

        public static GameManager instance;

        public int timeUntilGameStarts = 10;

        private NetworkManagerLobby _networkManagerLobby;
        
        [SyncVar]
        public bool hasGameStarted;

        private void Start()
        {
            _networkManagerLobby = (NetworkManagerLobby) NetworkManager.singleton;
        }

        private void Awake()
        {
            instance = this;
        }

        [Server]
        public IEnumerator AssignRoles()
        {
            yield return new WaitForSeconds(timeUntilGameStarts);

            for (int i = 0; i < alivePlayers.Count; i++)
            {
                if (i == 0)
                {
                    alivePlayers[i].SetRole(Role.Terrorist);
                    terroristPlayers.Add(alivePlayers[i]);
                }
                else
                {
                    alivePlayers[i].SetRole(Role.Innocent);
                    innocentPlayers.Add(alivePlayers[i]);
                }
            }

            hasGameStarted = true;
        }

        [Server]
        public void ServerKillPlayer(NetworkGamePlayer player)
        {
            alivePlayers.Remove(player);
            deadPlayers.Add(player);
            if (terroristPlayers.Contains(player)) terroristPlayers.Remove(player);
            if (innocentPlayers.Contains(player)) innocentPlayers.Remove(player);
            player.SetRole(Role.Dead);
            NetworkServer.ReplacePlayerForConnection(player.connectionToClient, _networkManagerLobby.deadPlayerPrefab);
            CheckPlayerLists();
        }

        [Server]
        private void CheckPlayerLists()
        {
            if (terroristPlayers.Count == 0)
            {
                Debug.Log("Innocents win!");
                StartCoroutine(GameOverRoutine(Role.Innocent));
                //networkManagerLobby.ServerChangeScene(networkManagerLobby.RoomScene);
            }

            if (innocentPlayers.Count == 0)
            {
                Debug.Log("Terroists win!");
                StartCoroutine(GameOverRoutine(Role.Terrorist));
                //networkManagerLobby.ServerChangeScene(networkManagerLobby.RoomScene);
            }
        }

        [Server]
        private IEnumerator GameOverRoutine(Role winningTeam)
        {
            yield return new WaitForSeconds(5);
            foreach (var networkGamePlayer in alivePlayers)
            {
                Destroy(networkGamePlayer);
                NetworkServer.Destroy(networkGamePlayer.gameObject);
            }
            foreach (var networkGamePlayer in deadPlayers)
            {
                Destroy(networkGamePlayer);
                NetworkServer.Destroy(networkGamePlayer.gameObject);
            }
            alivePlayers.Clear();
            deadPlayers.Clear();
            innocentPlayers.Clear();
            terroristPlayers.Clear();
            _networkManagerLobby.ServerChangeScene(_networkManagerLobby.RoomScene);
        }
    }
}