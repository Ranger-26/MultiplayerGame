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
        public static void WriteItemBase(this NetworkWriter writer, ItemBase item)
        {
            Debug.Log($"Calling write item.... with {item == null} null ref.");
            if (item == null)
            {
                writer.WriteInt(-1);
                return;
            }
            int index = _idsByItem[item];
            Debug.Log($"Writing int with value {index}");
            writer.WriteInt(index);
        }

        public static ItemBase ReadItemBase(this NetworkReader reader)
        {
            int index = reader.ReadInt();
            if (index == -1) return null;
            Debug.Log($"Reading int with value {index}");
            return _itemsById[index];
        }
        
        [RuntimeInitializeOnLoadMethod]
        public static void Initialize()
        {
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.BaseType == typeof(ItemBase))
                {
                    ItemBase itemBase = (ItemBase)type.GetConstructor(Type.EmptyTypes)?.Invoke(Array.Empty<object>());
                    _itemsById.Add((int)itemBase.Id, itemBase);
                    _idsByItem.Add(itemBase, (int)itemBase.Id);
                    Debug.Log($"Successfully registering ItemBase - {type.FullName} with id {(int)itemBase.Id}");
                }
            }

            foreach (var key in _idsByItem)
            {
                Debug.Log($"Key: {key.Value}");
            }
        }
    }
}