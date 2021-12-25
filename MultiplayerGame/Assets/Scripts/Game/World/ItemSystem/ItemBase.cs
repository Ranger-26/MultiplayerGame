using Game.Player;
using Mirror;
using UnityEngine;

namespace Game.World.ItemSystem
{
    public class ItemBase : ScriptableObject, ISerializer
    {
        public ItemType ItemType { get; set; }

        public virtual void Write(NetworkWriter writer, ItemBase item)
        {
            
        }

        public virtual ItemBase Read(NetworkReader reader, ItemType id)
        {
            return CreateInstance<ItemBase>();
        }

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