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
        [SyncVar] public Role curRole;

        [SyncVar] private int _health;

        [SyncVar(hook = nameof(OnTextChanged))] public string Name;

        public TextMesh nameText;

        public Text roleText;
        public override void OnStartLocalPlayer()
        {
            CmdSetName(PlayerPrefs.GetString("PlayerName"));
            base.OnStartLocalPlayer();
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

        [TargetRpc]
        public void TargetFlashText()
        {
            roleText.gameObject.SetActive(true);
        }
    }
}