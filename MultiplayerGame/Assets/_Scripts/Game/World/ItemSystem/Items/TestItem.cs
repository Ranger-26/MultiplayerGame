using Game.Player;
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
}