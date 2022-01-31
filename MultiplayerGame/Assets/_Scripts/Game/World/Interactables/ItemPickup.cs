using Game.ItemSystem.ItemScripts;
using Game.Player;
using Mirror;
using UnityEngine;

namespace Game.World.Interactables
{
    public class ItemPickup : NetworkBehaviour, IInteractable
    {
        [SyncVar]
        public ItemType item;

        public void Highlight()
        {
            GetComponent<Renderer>().material.SetColor("_Color",Color.yellow);
        }

        public void OnInteract(NetworkGamePlayer player)
        {
            player.GetComponent<Inventory>().CmdAddItem(item);
        }

        public void UnHighlight()
        {
            GetComponent<Renderer>().material.SetColor("_Color",Color.red);
        }
    }
}