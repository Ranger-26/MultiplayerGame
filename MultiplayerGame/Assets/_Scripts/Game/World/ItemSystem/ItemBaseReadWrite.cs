using System;
using System.Collections.Generic;
using System.Reflection;
using Mirror;
using UnityEngine;

namespace Game.World.ItemSystem
{
    public static class ItemBaseReadWrite
    {
        private static Dictionary<int, BaseSerializer> _itemSerializerById = new Dictionary<int, BaseSerializer>();
        public static void WriteItemBase(this NetworkWriter writer, ItemBase item)
        {
            Debug.Log($"Calling write item.... with {item == null} null ref.");
            if (item == null)
            {
                writer.WriteInt(-1);
                return;
            }
            int index = (int)item.Id;
            Debug.Log($"Writing int with value {index}");
            writer.WriteInt(index);
        }

        public static ItemBase ReadItemBase(this NetworkReader reader)
        {
            int index = reader.ReadInt();
            if (index == -1) return null;
            Debug.Log($"Reading int with value {index}");
            return _itemSerializerById[index].Read(reader, index);
        }
        
        [RuntimeInitializeOnLoadMethod]
        public static void Initialize()
        {
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.BaseType == typeof(BaseSerializer))
                {
                    try
                    {
                        BaseSerializer itemBaseSerializer = (BaseSerializer)type.GetConstructor(Type.EmptyTypes)?.Invoke(Array.Empty<object>());
                        _itemSerializerById.Add((int)itemBaseSerializer.Id, itemBaseSerializer);
                        Debug.Log($"Succesfully registered item {itemBaseSerializer.Id}");
                    }
                    catch (Exception e)
                    {
                        Debug.Log($"Error when registering item {type.FullName}: {e}");
                        throw; 
                    }
                }
            }
        }
    }
}