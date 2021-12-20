using System;
using Game.World.ItemSystem;
using Mirror;
using UnityEngine;

namespace Game.Player.Inventory
{
    public class Inventory : NetworkBehaviour
    {
        private SyncDictionary<int, ItemType> _inventory = new SyncDictionary<int, ItemType>();
        
        [SyncVar]
        private ItemType _curHeldItem = ItemType.None;

        [Command]
        public void CmdAddInventory(ItemType item)
        {
            if (_inventory.Count < 8)
            {
                _inventory.Add(_inventory.Count, item);
            }
        }

        private void Update()
        {
            if (_curHeldItem != ItemType.None && Input.GetKeyDown(KeyCode.Mouse0))
            {
                //use current held item
            }
        }
    }
}