using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace Game.World.ItemSystem
{
    public class ItemDatabase : NetworkBehaviour
    {
        public static ItemDatabase Instance;
        
        public Dictionary<ItemType, GameObject> itemDb = new Dictionary<ItemType, GameObject>();

        [SerializeField]
        private GameObject yes;
        private void Awake()
        {
            Instance = this;
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            itemDb.Add(ItemType.TestItem, yes);
        }
    }
}