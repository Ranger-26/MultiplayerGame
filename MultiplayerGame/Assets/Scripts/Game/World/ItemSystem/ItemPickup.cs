using Game.Player;
using Mirror;

namespace Game.World.ItemSystem
{
    public class ItemPickup : NetworkBehaviour, IInteractable
    {
        public void OnInteract(NetworkGamePlayer ply)
        {
            //add to inventory
        }
    }
}