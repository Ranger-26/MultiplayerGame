using Game.Player;
using Mirror;
using UnityEngine;

namespace Game.World.ItemSystem
{
    public class ItemBase : ScriptableObject
    {
        public ItemType Id { get; }

        public bool isReusable;

        public GameObject heldItem;

        public virtual void OnUse(NetworkGamePlayer ply)
        {
            
        }

        public virtual void OnEquipt(NetworkGamePlayer ply)
        {
            
        }
        
        public virtual void OnDequipt(NetworkGamePlayer ply)
        {
            
        }
    }
}