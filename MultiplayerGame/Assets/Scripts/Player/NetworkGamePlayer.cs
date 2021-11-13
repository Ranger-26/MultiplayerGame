using System;
using System.Collections.Generic;
using Game;
using Lobby;
using Mirror;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Player
{
    public class NetworkGamePlayer : NetworkBehaviour
    {
        [SerializeField]
        private Text _roleText;

        [SyncVar] private Role curRole;

        [SyncVar] private int _health;

        [SyncVar] public string Name;
        
        [TargetRpc]
        public void SetRole(Role newRole) => curRole = newRole;

        public override void OnStartLocalPlayer()
        {
            Camera.main.transform.SetParent(transform);
            Camera.main.transform.localPosition = new Vector3(0, 0, 0);
            GameManager.Instance.AddPlayer(this);
            CmdSetName(Random.Range(0, 100).ToString());
            foreach (var instanceAlivePlayer in GameManager.Instance.alivePlayers)
            {
                Debug.Log(instanceAlivePlayer.Name + "is in the game!");
            }
        }

        public void ShowRoleText()
        {
            _roleText.text = "You are a " + curRole;
            _roleText.gameObject.SetActive(true);
        }

        [Command]
        public void CmdSetName(string name)
        {
            this.Name = name;
        } 
    }
}