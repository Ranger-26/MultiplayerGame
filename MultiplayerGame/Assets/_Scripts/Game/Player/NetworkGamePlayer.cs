using Game.GameLogic;
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

        public Text roleText;

        public Text gameOverText;

        public HealthHandler healthController;

        public Inventory.Inventory inventory;

        private void Start()
        {
            healthController = GetComponent<HealthHandler>();
            inventory = GetComponent<Inventory.Inventory>();
        }

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
            roleText.text = $"You are a {role}";
            roleText.gameObject.SetActive(true);
        }

        [ClientRpc]
        public void RpcShowGameOverScreen(string text)
        {
            gameOverText.text = text;
            gameOverText.gameObject.SetActive(true);
        }
    }
}