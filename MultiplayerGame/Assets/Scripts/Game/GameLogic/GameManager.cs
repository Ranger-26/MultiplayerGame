using System;
using System.Collections;
using System.Collections.Generic;
using Lobby;
using Mirror;
using Player;
using UnityEditor;
using UnityEngine;

namespace Game
{
    public class GameManager : NetworkBehaviour
    {
        public List<NetworkGamePlayer> alivePlayers = new List<NetworkGamePlayer>();
        
        public List<NetworkGamePlayer> deadPlayers = new List<NetworkGamePlayer>();

        public List<NetworkGamePlayer> innocentPlayers = new List<NetworkGamePlayer>();

        public List<NetworkGamePlayer> terroristPlayers = new List<NetworkGamePlayer>();

        public static GameManager instance;
        
        public int numTraitors = 1;

        public int timeUntilGameStarts = 10;

        [SyncVar]
        public bool hasGameStarted;
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
            player.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}