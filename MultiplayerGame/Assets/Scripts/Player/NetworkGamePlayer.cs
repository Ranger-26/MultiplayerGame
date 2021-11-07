using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace Player
{
    public class NetworkGamePlayer : NetworkBehaviour
    {
        [SyncVar] private Role curRole;

        [SyncVar] private int _health;

        public void SetRole(Role newRole) => curRole = newRole;

        public override void OnStartLocalPlayer()
        {
            Camera.main.transform.SetParent(transform);
            Camera.main.transform.localPosition = new Vector3(0, 0, 0);
        }

    }
}