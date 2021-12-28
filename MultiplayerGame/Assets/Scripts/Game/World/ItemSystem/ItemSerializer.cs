using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace Game.World.ItemSystem
{
    public static class ItemSerializer
    {
        private static Dictionary<int, ItemBase> _spellsById = new Dictionary<int, ItemBase>();
        private static Dictionary<ItemBase, int> _idsBySpell = new Dictionary<ItemBase, int>();
        
        [RuntimeInitializeOnLoadMethod]
        public static void Initialize()
        {
            // load all spells
            ItemBase[] allSpells = Resources.LoadAll<ItemBase>("your file path");

            foreach(ItemBase item in allSpells)
            {
                _spellsById.Add(item.Id, item);
                _idsBySpell.Add(item, item.Id);
            }
        }
        
        public static void WriteSpell(this ItemBase item, NetworkWriter writer)
        {
            writer.WriteInt(_idsBySpell[item]);
        }
        
        public static ItemBase ReadSpell(this NetworkReader reader)
        {
            return _spellsById[reader.ReadInt()];
        }
    }
}