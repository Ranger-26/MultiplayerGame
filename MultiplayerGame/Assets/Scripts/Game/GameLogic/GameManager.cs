using System;
using System.Collections;
using System.Collections.Generic;
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

        public static GameManager Instance;
        
        public int numTraitors = 1;

        [SyncVar(hook = nameof(OnTimeChanged))]
        public int timeUntilGameStarts = 10;

        public bool hasGameStarted;
        private void Awake()
        {
            Instance = this;
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
        }

        public void OnTimeChanged(int _, int neww)
        {
            foreach (var player in alivePlayers)
            {
                if (timeUntilGameStarts == 0)
                {
                    player.timer.gameObject.SetActive(false);
                }
                else
                {
                    player.timer.text = $"Time until game starts: {neww}";
                }
            }
        }
    }
}