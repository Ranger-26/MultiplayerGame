using System;
using Game.World.ItemSystem;
using Mirror;
using UnityEngine;

namespace Game.Player.Inventory
{
    public class Inventory : NetworkBehaviour
    {
        private SyncList<ItemBase> _inventory = new SyncList<ItemBase>();
        
        [SyncVar]
        private ItemBase _curHeldItem = null;

        [Command]
        public void CmdAddItem(ItemBase item)
        {
            _inventory.Add(item);
            TargetAddItem(item);
        }

        [TargetRpc]
        private void TargetAddItem(ItemBase item)
        {
            Debug.Log("Added item!");
        }
        private void Update()
        {
            if (_curHeldItem == null) return;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _curHeldItem.OnUse(GetComponent<NetworkGamePlayer>());
            }
        }
    }
}