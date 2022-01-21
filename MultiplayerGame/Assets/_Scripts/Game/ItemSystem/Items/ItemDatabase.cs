using System.Collections;
using UnityEngine;
using Mirror;

namespace Assets._Scripts.Game.ItemSystem.Items
{
    public class ItemDatabase : NetworkBehaviour
    {
        private void Awake()
        {
           if (instance != null)
           {
                Destroy(this);
           }
            instance = this;
        }

        public static ItemDatabase instance;

        public SyncDictionary<ItemType, BaseItem> idToItems = new SyncDictionary<ItemType, BaseItem>();

        public override void OnStartServer()
        {
            //set items
        }
    }

    public struct ItemInfo
    {
        public GameObject gameObject;

        public BaseItem baseItem;
    }
}