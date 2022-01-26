using Game.ItemSystem.ItemScripts;
using Game.Player;
using Mirror;
using UnityEditor;
using UnityEngine;

namespace Assets._Scripts.Game.ItemSystem.Items
{
    [CreateAssetMenu(fileName = "TestItem", menuName = "BaseItem/TestItem")]
    public class TestItem : BaseItem
    {
        public override ItemType Id { get; } = ItemType.TestItem;

        public override void OnUse(NetworkGamePlayer ply)
        {
            Debug.Log($"Player {ply.Name} has used the test item!");
        }
    }

    public class TestItemSerializer : BaseSerializer
    {
        public override ItemType Id { get; } = ItemType.TestItem;

        public override BaseItem Read(NetworkReader reader, int id)
        {
            return ScriptableObject.CreateInstance<TestItem>();
        }

        public override void Write(NetworkWriter writer, BaseItem item)
        {
            if (item.GetType() == typeof(TestItem))
            {
                TestItem newItem = (TestItem)item;
            }
        }
    }
}