using Game.ItemSystem.ItemScripts;
using Game.Player;
using UnityEditor;
using UnityEngine;

namespace Assets._Scripts.Game.ItemSystem.Items
{
    [CreateAssetMenu(fileName = "TestItem", menuName = "BaseItem/TestItem")]
    public class TestItem : BaseItem
    {
        public override void OnUse(NetworkGamePlayer ply)
        {
            Debug.Log($"Player {ply.Name} has used the test item!");
        }
    }
}