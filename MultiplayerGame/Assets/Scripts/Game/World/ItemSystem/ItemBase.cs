using Game.Player;
using Mirror;
using UnityEngine;

namespace Game.World.ItemSystem
{
    public class ItemBase : ScriptableObject
    {
        public ItemType ItemType { get; set; }
        
        public int Id { get; }
        public virtual bool OnPickup(NetworkGamePlayer ply)
        {
            return true;
        }

        public virtual bool OnEquipt(NetworkGamePlayer ply)
        {
            return true;
        }

        public virtual bool OnUse(NetworkGamePlayer ply)
        {
            return true;
        }
    }
}