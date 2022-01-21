using Mirror;
using UnityEditor;
using UnityEngine;

namespace Assets._Scripts.Game.ItemSystem.Items
{
    public abstract class BaseSerializer
    {
        public abstract ItemType Id { get; }

        public abstract void Write(NetworkWriter writer, BaseItem item);

        public abstract BaseItem Read(NetworkReader reader, int id);
    }
}