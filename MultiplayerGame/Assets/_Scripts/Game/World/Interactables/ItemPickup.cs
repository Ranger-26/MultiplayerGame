using System.Collections;
using UnityEngine;
using Mirror;
using Game.World;
using Game.Player;
using Assets._Scripts.Game.ItemSystem;
using Game.ItemSystem.ItemScripts;

namespace Assets._Scripts.Game.World.Interactables
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
            //player.inventory.CmdAddItem(item);
            if (player.GetComponent<Inventory>() == null || item == null) Debug.Log("Player inventory is null!");
            player.GetComponent<Inventory>().CmdAddItem(item);
        }

        public void UnHighlight()
        {
            GetComponent<Renderer>().material.SetColor("_Color",Color.red);
        }
    }
}