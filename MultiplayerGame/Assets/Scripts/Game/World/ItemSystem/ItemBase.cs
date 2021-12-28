using Game.Player;
using Mirror;
using UnityEngine;

namespace Game.World.ItemSystem
{
    public class ItemBase : ScriptableObject
    {
        public ItemType Id { get; }

        public bool isReuseable;
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
            if (!isReuseable)
            {
                    
            }
            return true;
        }
    }
}