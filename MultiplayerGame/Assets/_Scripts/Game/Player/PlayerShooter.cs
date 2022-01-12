using System.Collections;
using Mirror;
using UnityEngine;

namespace Game.Player
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
            if (Input.GetKeyDown(KeyCode.Mouse0) && _canShoot)
            {
                Shoot();
            }
        }

        void Shoot()
        {
            if (hasAuthority)
            {
                CmdShoot();
                _canShoot = false;
            }
            StartCoroutine(CooldownRoutine());
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