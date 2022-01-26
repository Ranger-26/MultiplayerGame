using Game.ItemSystem.ItemScripts;
using Mirror;
using UnityEngine;

namespace Game.Player
{
    public class Inventory : NetworkBehaviour
    {
        [SyncVar]
        private ItemType curItem;

        private SyncList<ItemType> inventory = new SyncList<ItemType>();

        [SyncVar]
        private int maxItems = 8;

        private NetworkGamePlayer player;
        // Use this for initialization
        void Start()
        {
            player = GetComponent<NetworkGamePlayer>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                CmdUseHeldItem();
            }
        }

        [Command]
        private void CmdUseHeldItem()
        {
            Debug.Log($"Player {player.Name} using item {curItem}");
            BaseItem item = ItemDatabase.Instance.idToItems[(int)curItem];
            item.OnUse(player);
        }

        [Command]
        public void CmdAddItem(ItemType item)
        {
            Debug.Log($"Trying to add item {item} for player {player.Name}! Inventory has {inventory.Count} items!");
            if (inventory.Count >= maxItems) return;
            inventory.Add(item);
            curItem = item;
            Debug.Log($"Added item {item} for player {player.Name}! Inventory has {inventory.Count} items!");
        }
    }
}