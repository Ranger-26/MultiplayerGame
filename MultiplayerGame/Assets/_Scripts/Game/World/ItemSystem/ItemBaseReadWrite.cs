using System;
using System.Collections.Generic;
using System.Reflection;
using Mirror;
using UnityEngine;

namespace Game.World.ItemSystem
{
    /*
    public static class ItemBaseReadWrite
    {
        private static Dictionary<int, ItemBase> _itemsById = new Dictionary<int, ItemBase>();
        
        private static Dictionary<ItemBase, int> _idsByItem = new Dictionary<ItemBase, int>();

        private const string itemsFolder = "ItemObjects";
        public static void WriteItemBase(this ItemBase item, NetworkWriter writer)
        {
            Debug.Log($"Writing item with id {_idsByItem[item]}");
            writer.WriteInt(_idsByItem[item]);
        }

        public static ItemBase ReadItemBase(this NetworkReader reader)
        {
            Debug.Log($"Returning item with id {reader.ReadInt()}");
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
                        Debug.Log($"Successfully registering ItemBase - {type.FullName} with id {(int)itemBase.Id}");
                    }
                    catch (Exception ex)
                    {
                        Debug.Log("Error registering ItemBase - " + type.FullName + " - " + ex);
                    }
                }
            }

            foreach (var key in _idsByItem)
            {
                Debug.Log($"Key: {key.Value}");
            }
        }
    }
    */
}