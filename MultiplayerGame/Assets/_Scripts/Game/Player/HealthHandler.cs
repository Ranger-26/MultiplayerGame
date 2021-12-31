using Assets.Scripts.Game;
using Game.GameLogic;
using Mirror;
using UnityEngine;

namespace Game.Player
{
    public class HealthHandler : NetworkBehaviour, IDamageable
    {
        [SerializeField]
        [SyncVar]
        public int curHealth = 100;

        [SerializeField]
        private AudioSource getShotClip;

        [Command]
        public void CmdDamage(int damage)
        {
            if (!GameManager.instance.hasGameStarted) return;
            curHealth = curHealth - damage >= 0 ? curHealth - damage : 0;
            if (curHealth > 0)
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
            getShotClip?.Play();
        }

        [TargetRpc]
        public void TargetDamagePlayer(NetworkConnection conn)
        {
            Debug.Log($"Getting damaged... new health is now {curHealth}.");
        }

        [TargetRpc]
        public void Damage(int damage) => CmdDamage(damage);
    }
}