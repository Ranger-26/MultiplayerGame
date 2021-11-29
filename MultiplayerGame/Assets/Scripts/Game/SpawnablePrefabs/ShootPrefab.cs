using Mirror;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Game.SpawnablePrefabs
{
    public class ShootPrefab : NetworkBehaviour
    {
        public float movementSpeed = 5;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("I hit something!");

            //Destroy(this.gameObject);
            //NetworkServer.Destroy(this.gameObject);
        }
    }
}