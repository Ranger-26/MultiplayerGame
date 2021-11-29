using Mirror;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class HealthController : NetworkBehaviour
    {
        [SyncVar]
        private int _curHealth = 100;   

        [Command]
        public void CmdDamage(int damage)
        {
            _curHealth -= damage;
        }

        [TargetRpc]
        public void RpcDamagePlayer(int damage) => Debug.Log("You have been damaged by ");

        public void Damage(int damage) => CmdDamage(damage);
    }
}