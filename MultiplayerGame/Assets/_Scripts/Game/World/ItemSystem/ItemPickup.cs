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
            ply.inventory.CmdAddItem(item.Clone());
            Destroy(gameObject);
        }

        public void Highlight()
        {
            GetComponent<Renderer>().material.SetColor("_Color",Color.yellow);
        }

        public void UnHighlight()
        {
            GetComponent<Renderer>().material.SetColor("_Color",Color.red);
        }
    }
}