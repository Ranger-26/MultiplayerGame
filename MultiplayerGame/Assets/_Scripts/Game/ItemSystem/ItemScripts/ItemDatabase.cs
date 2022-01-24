using Mirror;
using UnityEngine;

namespace Game.ItemSystem.ItemScripts
{
    public class ItemDatabase : NetworkBehaviour
    {
        private void Awake()
        {
           if (Instance != null)
           {
                Destroy(this);
           }
           Instance = this;
        }

        public static ItemDatabase Instance;

        public SyncDictionary<int, BaseItem> idToItems = new SyncDictionary<int, BaseItem>();

        public override void OnStartServer()
        {
            BaseItem[] items = Resources.LoadAll<BaseItem>("ItemObjects");
            foreach(var item in items)
            {
                idToItems.Add((int)item.Id, item);
            }
        }
    }
}