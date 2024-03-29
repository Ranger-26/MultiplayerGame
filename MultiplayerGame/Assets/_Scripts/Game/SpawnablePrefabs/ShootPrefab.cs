﻿using Mirror;
using UnityEngine;

namespace Game.SpawnablePrefabs
{
    public class ShootPrefab : NetworkBehaviour
    {
        public float movementSpeed = 5;

        private int Damage = 25;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
        }

        [ServerCallback]
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("I hit something!");

            if (collision.collider.TryGetComponent(out IDamageable target))
            {
                target.Damage(Damage);
                Destroy(gameObject);
            }
            //Destroy(this.gameObject);
            //NetworkServer.Destroy(this.gameObject);
        }
    }
}