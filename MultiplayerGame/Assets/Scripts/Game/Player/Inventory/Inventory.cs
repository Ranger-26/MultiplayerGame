using System;
using Game.World.ItemSystem;
using Mirror;
using UnityEngine;

namespace Game.Player.Inventory
{
    public class Inventory : NetworkBehaviour
    {
        private SyncDictionary<int, ItemBase> _inventory = new SyncDictionary<int, ItemBase>();
        
        [SyncVar]
        private ItemType _curHeldItem = ItemType.None;
    }
}