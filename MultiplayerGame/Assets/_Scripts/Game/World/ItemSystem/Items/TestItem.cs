using Game.Player;
using Mirror;
using UnityEngine;

namespace Game.World.ItemSystem.Items
{
    [CreateAssetMenu(fileName = "TestItem", menuName = "ItemBases/TestItem", order = 1)]
    public class TestItem : ItemBase
    {
        public override void OnUse(NetworkGamePlayer ply)
        {
            Debug.Log($"Player {ply.name} has used the test item!");
        }

        public override void OnEquipt(NetworkGamePlayer ply)
        {
            base.OnEquipt(ply);
            Debug.Log($"Player {ply.name} has equpted the test item!");
        }

        public override void OnDequipt(NetworkGamePlayer ply)
        {
            base.OnDequipt(ply);
            Debug.Log($"Player {ply.name} has dequpted the test item!");
        }
    }

    public class TestItemSerializer : BaseSerializer
    {
        public override ItemType Id { get; } = ItemType.TestItem;
        public override void Write(NetworkWriter writer, ItemBase item)
        {
            
        }

        public override ItemBase Read(NetworkReader reader, int id)
        {
            return ScriptableObject.CreateInstance<TestItem>();
        }
    }
}