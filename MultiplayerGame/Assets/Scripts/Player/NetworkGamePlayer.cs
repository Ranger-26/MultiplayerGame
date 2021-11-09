using System;
using System.Collections.Generic;
using Lobby;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class NetworkGamePlayer : NetworkBehaviour
    {
        [SerializeField]
        private Text _roleText;

        [SyncVar] private Role curRole;

        [SyncVar] private int _health;

        [Server]
        public void SetRole(Role newRole) => curRole = newRole;

        public override void OnStartLocalPlayer()
        {
            Camera.main.transform.SetParent(transform);
            Camera.main.transform.localPosition = new Vector3(0, 0, 0);
            
        }

        public void ShowRoleText()
        {
            _roleText.text = "You are a " + curRole;
            _roleText.gameObject.SetActive(true);
        }
    }
}