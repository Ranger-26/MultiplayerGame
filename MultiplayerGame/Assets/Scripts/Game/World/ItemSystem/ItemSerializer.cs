using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;

namespace Game.World.ItemSystem
{
    public static class ItemSerializer
    {
        private static Dictionary<int, ItemBase> _ItemBasesById = new Dictionary<int, ItemBase>();
        private static Dictionary<ItemBase, int> _idsByItemBase = new Dictionary<ItemBase, int>();

        // this attribute makes unity invoke the 
        // method on game startup
        [RuntimeInitializeOnLoadMethod]
        public static void Initialize()
        {
            // load all ItemBases
            ItemBase[] allItemBases = Resources.LoadAll<ItemBase>("your file path");

            foreach(ItemBase ItemBase in allItemBases)
            {
                _ItemBasesById.Add((int)ItemBase.Id, ItemBase);
                _idsByItemBase.Add(ItemBase, (int)ItemBase.Id);
            }
        }


        public static void WriteItemBase(this ItemBase ItemBase, NetworkWriter writer)
        {
            writer.WriteInt(_idsByItemBase[ItemBase]);
        }

        public static ItemBase ReadItemBase(this NetworkReader reader)
        {
            return _ItemBasesById[reader.ReadInt()];
        }
    }
}