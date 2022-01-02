using System;
using Game.World.ItemSystem;
using Mirror;
using UnityEditor;
using UnityEngine;

namespace Game.Player.Inventory
{
    public class Inventory : NetworkBehaviour
    {
        private SyncList<ItemBase> _inventory = new SyncList<ItemBase>();
        
        [SyncVar]
        private ItemBase _curHeldItem = null;

        private NetworkGamePlayer player;

        [SerializeField]
        private GameObject itemSpawnPoint;
        private void Start()
        {
            player = GetComponent<NetworkGamePlayer>();
        }

        [Command]
        public void CmdAddItem(ItemBase item)
        {
            _inventory.Add(item);
            TargetAddItem(item);
        }

        [TargetRpc]
        private void TargetAddItem(ItemBase item)
        {
            Debug.Log("Added item!");
        }

        [Command]
        private void CmdEquiptItem(ItemBase item)
        {
            Debug.Log($"Calling equipt item...{item.heldItem == null}");
            GameObject heldItem = Instantiate(ItemDatabase.Instance.itemDb[item.Id], itemSpawnPoint.transform.position, Quaternion.identity);
            heldItem.transform.SetParent(player.transform);
            NetworkServer.Spawn(heldItem);
            _curHeldItem = item;
            _curHeldItem.OnEquipt(player);
        }
        
        private void Update()
        {
            //if (_curHeldItem == null) return;
            
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _curHeldItem?.OnUse(GetComponent<NetworkGamePlayer>());
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                Debug.Log("Trying to call CmdEquiptItem....");
                CmdEquiptItem(_inventory[0]);
            }
        }
    }
}