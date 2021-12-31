using Game.Player;
using Mirror;
using UnityEngine;

namespace Game.World.ItemSystem
{
    public class ItemPickup : NetworkBehaviour, IInteractable
    {
        public ItemBase item;
        public void OnInteract(NetworkGamePlayer ply)
        {
            ply.inventory.CmdAddItem(item);
            Destroy(gameObject);
        }
    }
}