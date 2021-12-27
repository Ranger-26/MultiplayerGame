using Game.Player;
using Mirror;
using UnityEngine;

namespace Game.World.ItemSystem
{
    public class ItemPickup : NetworkBehaviour, IInteractable
    {
        [SerializeField]
        private ItemBase itemBase;
        public void OnInteract(NetworkGamePlayer ply)
        {
           ply.inventory.CmdAddItem(itemBase);
        }
    }
}