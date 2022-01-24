using System.Collections;
using UnityEngine;
using Mirror;
using Game.World;
using Game.Player;
using Assets._Scripts.Game.ItemSystem;

namespace Assets._Scripts.Game.World.Interactables
{
    public class ItemPickup : NetworkBehaviour, IInteractable
    {
        [SyncVar]
        public ItemType item;

        public void Highlight()
        {
            
        }

        [Command]
        public void OnInteract(NetworkGamePlayer player)
        {
            player.inventory.CmdAddItem(item);
        }

        public void UnHighlight()
        {
            
        }
    }
}