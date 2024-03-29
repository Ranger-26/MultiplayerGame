﻿using Game.GameLogic;
using Mirror;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Player
{
    public class NetworkGamePlayer : NetworkBehaviour
    {   
        [SerializeField]
        [SyncVar] private Role curRole;

        [SyncVar] private int _health;

        [SyncVar(hook = nameof(OnTextChanged))]
        public string Name;

        [Header("Text")]
        public TextMesh nameText;

        public HealthHandler healthController;

        public Inventory inventory;

        [SerializeField]
        private UIManager _uiManager;

        private void Start()
        {
            healthController = GetComponent<HealthHandler>();
            inventory = GetComponent<Inventory>();
        }

        public override void OnStartLocalPlayer()
        {
            CmdSetName(PlayerPrefs.GetString("PlayerName"));
            Camera.main.transform.SetParent(transform.GetChild(2));
            Camera.main.transform.localPosition = new Vector3(0, 0, 0);
            _uiManager.UpdateHealth(100);
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
            curRole = newRole;
        }

        [TargetRpc]
        public void SetRole(Role role)
        {
            if (!isServer)
            {
                Debug.Log($"Client: Setting Role {role}");
            }
            CmdSetRole(role);
            _uiManager.UpdateRoleText(role);
        }

        [ClientRpc]
        public void RpcShowGameOverScreen(string text)
        {
            _uiManager.UpdateGameOverText(text);
        }
    }
}