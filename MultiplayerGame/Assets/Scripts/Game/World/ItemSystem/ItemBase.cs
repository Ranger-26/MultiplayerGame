using Game.Player;
using UnityEngine;

namespace Game.World.ItemSystem
{
    public class ItemBase : ScriptableObject
    {
        public ItemType ItemType { get; set; }
        
        public GameObject heldItem;
    }
}