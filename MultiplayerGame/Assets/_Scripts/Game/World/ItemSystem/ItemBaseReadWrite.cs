using System;
using System.Collections.Generic;
using System.Reflection;
using Mirror;
using UnityEngine;

namespace Game.World.ItemSystem
{
    public static class ItemBaseReadWrite
    {
        private static Dictionary<int, ItemBase> _itemsById = new Dictionary<int, ItemBase>();
        
        private static Dictionary<ItemBase, int> _idsByItem = new Dictionary<ItemBase, int>();
        
        public static void WriteItemBase(this ItemBase spell, NetworkWriter writer)
        {
            writer.WriteInt(_idsByItem[spell]);
        }

        public static ItemBase ReadItemBase(this NetworkReader reader)
        {
            return _itemsById[reader.ReadInt()];
        }
        
        [RuntimeInitializeOnLoadMethod]
        public static void Initialize()
        {
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.BaseType == typeof(ItemBase))
                {
                    try
                    {
                        ItemBase itemBase = (ItemBase)type.GetConstructor(Type.EmptyTypes)?.Invoke(new object[0]);
                        _itemsById.Add((int)itemBase.Id, itemBase);
                        _idsByItem.Add(itemBase, (int)itemBase.Id);
                        Debug.Log("Successfully registering ItemBaseSerializer - " + type.FullName);
                    }
                    catch (Exception ex)
                    {
                        Debug.Log("Error registering ItemBaseSerializer - " + type.FullName + " - " + ex.ToString());
                    }
                }
            }
        }
    }
}