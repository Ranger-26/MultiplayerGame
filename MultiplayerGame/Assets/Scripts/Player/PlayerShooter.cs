using Mirror;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerShooter : NetworkBehaviour
    {
        [SerializeField]
        private GameObject shootingPrefab;

        private int _cooldown = 1;

        [SerializeField]
        private bool _canShoot = true;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shoot();
                StartCoroutine(CooldownRoutine());
            }
        }

        void Shoot()
        {
            if (_canShoot)
            {
                CmdShoot();
                _canShoot = false;
            }
        }

        [Command]
        private void CmdShoot() 
        {
            Vector3 deansSuggestion = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
            GameObject prefab = Instantiate(shootingPrefab, transform.position, Quaternion.identity);
            prefab.GetComponent<Rigidbody>().velocity = transform.forward * 5;
            NetworkServer.Spawn(prefab);

        }

        IEnumerator CooldownRoutine()
        {
            yield return new WaitForSeconds(_cooldown);
            _canShoot = true;
        }
    }
}