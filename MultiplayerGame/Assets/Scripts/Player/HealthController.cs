using Assets.Scripts.Game;
using Game;
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
                TargetDamagePlayer();
                return;
            }
            GameManager.instance.ServerKillPlayer(GetComponent<NetworkGamePlayer>());
            TargetKillPlayer();
        }

        [ClientRpc]
        public void RpcDamagePlayer()
        {
            _getShotClip?.Play();
        }

        [TargetRpc]
        public void TargetDamagePlayer()
        {
            Debug.Log($"Getting damaged... new health is now {_curHealth}.");
        }
        
        [TargetRpc]
        public void TargetKillPlayer()
        {
            Debug.Log("You are dead!");
        }

        [TargetRpc]
        public void Damage(int damage) => CmdDamage(damage);
    }
}