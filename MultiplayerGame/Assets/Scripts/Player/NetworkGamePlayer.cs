using System;
using System.Collections;
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
        [SyncVar] private Role curRole;

        [SyncVar] private int _health;

        [SyncVar(hook = nameof(OnTextChanged))]
        public string Name;

        public TextMesh nameText;

        public Text roleText;

        public Text timer;
        public override void OnStartLocalPlayer()
        {
            CmdSetName(PlayerPrefs.GetString("PlayerName"));
            Camera.main.transform.SetParent(transform.GetChild(2));
            Camera.main.transform.localPosition = new Vector3(0, 0, 0);
        }

        private void OnTextChanged(string _, string yes)
        {
            nameText.text = yes;
        }

        [Command]
        private void CmdSetName(string name)
        {
            Name = name;
        }

        [Command]
        private void CmdSetRole(Role newRole)
        {
            Debug.Log($"Server: Setting Role {newRole}");

            curRole = newRole;
            Debug.Log($"Server: New role is {newRole}");
        }

        [TargetRpc]
        public void SetRole(Role role)
        {
            if (!isServer)
            {
                Debug.Log($"Client: Setting Role {role}");
            }
            CmdSetRole(role);
            roleText.text = $"You are a {role}";
            roleText.gameObject.SetActive(true);
        }
    }
}