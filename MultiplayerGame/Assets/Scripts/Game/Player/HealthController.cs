using Assets.Scripts.Game;
using Game;
using Game.GameLogic;
using Game.Player;
using Mirror;
using Player;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class HealthController : NetworkBehaviour, IDamageable
    {
        [SerializeField]
        [SyncVar]
        private int _curHealth = 100;

        [SerializeField]
        private AudioSource _getShotClip;

        [Command]
        public void CmdDamage(int damage)
        {
            if (!GameManager.instance.hasGameStarted) return;
            _curHealth = _curHealth - damage >= 0 ? _curHealth - damage : 0;
            if (_curHealth > 0)
            {
                RpcDamagePlayer();
                TargetDamagePlayer(netIdentity.connectionToClient);
                return;
            }
            GameManager.instance.ServerKillPlayer(GetComponent<NetworkGamePlayer>());
        }

        [ClientRpc]
        public void RpcDamagePlayer()
        {
            _getShotClip?.Play();
        }

        [TargetRpc]
        public void TargetDamagePlayer(NetworkConnection conn)
        {
            Debug.Log($"Getting damaged... new health is now {_curHealth}.");
        }

        [TargetRpc]
        public void Damage(int damage) => CmdDamage(damage);
    }
}