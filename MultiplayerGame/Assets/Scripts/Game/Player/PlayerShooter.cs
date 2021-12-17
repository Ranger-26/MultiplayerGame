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

        [SerializeField]
        private AudioSource _audioSource;

        public GameObject gun;
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
            if (_canShoot && hasAuthority)
            {
                CmdShoot();
                _canShoot = false;
            }
        }

        [Command]
        private void CmdShoot() 
        {
            GameObject prefab = Instantiate(shootingPrefab, gun.transform.position, Quaternion.identity);
            prefab.GetComponent<Rigidbody>().velocity = transform.forward * 5;
            NetworkServer.Spawn(prefab);
            RpcShoot();
        }

        [ClientRpc]
        private void RpcShoot()
        {
            _audioSource.Play();
        }

        IEnumerator CooldownRoutine()
        {
            yield return new WaitForSeconds(_cooldown);
            _canShoot = true;
        }
    }
}