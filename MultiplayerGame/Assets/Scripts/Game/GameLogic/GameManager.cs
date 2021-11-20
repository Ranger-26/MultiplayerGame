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

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            
        }

       [Server]
        public IEnumerator Test()
        {
            foreach (var player in alivePlayers)
            {
                yield return new WaitForSeconds(10);
                player.TargetFlashText();
            }
        }

        public void yes()
        {
            StartCoroutine(Test());
        }
    }
}