using Assets.Scripts.Game;
using Mirror;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class HealthController : NetworkBehaviour, IDamageable
    {
        [SyncVar]
        private int _curHealth = 100;

        [SerializeField]
        private AudioSource _getShotClip;

        [Command]
        public void CmdDamage(int damage)
        {
            _curHealth -= damage;
            RpcDamagePlayer();
        }

        [ClientRpc]
        public void RpcDamagePlayer()
        {
            Debug.Log($"Getting damaged... new health is now {_curHealth}.");
            _getShotClip?.Play();
        }
        

        public void Damage(int damage) => CmdDamage(damage);
    }
}