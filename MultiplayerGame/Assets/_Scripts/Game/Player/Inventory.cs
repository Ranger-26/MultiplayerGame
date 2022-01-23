using Game.ItemSystem.ItemScripts;
using Mirror;
using UnityEngine;
using ItemType = Assets._Scripts.Game.ItemSystem.ItemType;

namespace Game.Player
{
    public class Inventory : NetworkBehaviour
    {
        [SyncVar]
        private ItemType curItem;

        private SyncList<ItemType> inventory = new SyncList<ItemType>();
        
        private NetworkGamePlayer player;
        // Use this for initialization
        void Start()
        {
            player = GetComponent<NetworkGamePlayer>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        [Command]
        private void CmdUseHeldItem()
        {
            BaseItem item = ItemDatabase.Instance.idToItems[(int)curItem];
            item.OnUse(player);
        }
    }
}