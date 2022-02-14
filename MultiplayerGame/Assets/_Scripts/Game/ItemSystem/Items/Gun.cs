using Game.ItemSystem.ItemScripts;
using Game.Player;
using Mirror;
using System.Collections;
using UnityEngine;

namespace Assets._Scripts.Game.ItemSystem.Items
{
    [CreateAssetMenu(fileName = "Gun", menuName = "BaseItem/Gun")]
    public class Gun : BaseItem
    {
        public override ItemType Id { get; } = ItemType.Gun;

        public int gunRange;

        public int baseDamage;

        public override void OnEquipt(NetworkGamePlayer ply)
        {
            
        }
    }

    public class GunSerializer : BaseSerializer
    {
        public override ItemType Id => ItemType.Gun;

        public override void Write(NetworkWriter writer, BaseItem item)
        {
            if (item.GetType() == typeof(BaseItem))
            {
                Gun itemType = (Gun)item;
                writer.WriteInt(itemType.gunRange);
                writer.WriteInt(itemType.baseDamage);
            }
        }

        public override BaseItem Read(NetworkReader reader, int id)
        {
            Gun gunItem = ScriptableObject.CreateInstance<Gun>();
            gunItem.gunRange = reader.ReadInt();
            gunItem.baseDamage = reader.ReadInt();
            return gunItem;
        }
    }
}