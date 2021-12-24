using Game.Player;
using Mirror;
using UnityEngine;

namespace Game.World.ItemSystem
{
    public class ItemPickup : NetworkBehaviour, IInteractable
    {
        [SerializeField]
        private ItemType itemType;
        public void OnInteract(NetworkGamePlayer ply)
        {
           
        }

        [Command]
        private void CmdInteract(NetworkGamePlayer ply)
        {
            
        }
    }
}