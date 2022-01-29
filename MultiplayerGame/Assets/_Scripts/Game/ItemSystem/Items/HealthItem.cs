using Game.ItemSystem.ItemScripts;
using Game.Player;
using Mirror;
using UnityEngine;

namespace Game.ItemSystem.Items
{
    [CreateAssetMenu(fileName = "TestItem", menuName = "BaseItem/HealthItem")]
    public class HealthItem : BaseItem
    {
        public int health;

        public override ItemType Id { get; } = ItemType.HealthItem;

        public override void OnUse(NetworkGamePlayer ply)
        {
            ply.healthController.ServerHeal(health);
            Debug.Log($"{ply.Name} has {ply.healthController.curHealth} now!");
        }
    }

    public class HealthItemSerializer : BaseSerializer
    {
        public override ItemType Id { get; } = ItemType.HealthItem;
        
        public override void Write(NetworkWriter writer, BaseItem item)
        {
            if (item.GetType() == typeof(BaseItem))
            {
                HealthItem itemType = (HealthItem) item;
                writer.WriteInt(itemType.health);
            }
        }

        public override BaseItem Read(NetworkReader reader, int id)
        {
            HealthItem healthItem = ScriptableObject.CreateInstance<HealthItem>();
            healthItem.health = reader.ReadInt();
            return healthItem;
        }
    }
}