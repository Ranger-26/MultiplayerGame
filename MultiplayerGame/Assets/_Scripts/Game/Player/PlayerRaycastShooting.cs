using System.Collections;
using UnityEngine;
using Mirror;
using Game;

namespace Assets._Scripts.Game.Player
{
    public class PlayerRaycastShooting : NetworkBehaviour
    {
        [SyncVar][SerializeField]
        private int _gunRange = 15;

        [SyncVar][SerializeField]
        private int _damage = 25;

        [SerializeField]
        private AudioSource _shootSound;

        private int _cooldown = 1;

        private bool _canShoot = true;

        [SerializeField]
        private GameObject _shootParticle;

        void Start()
        {

        }

        private void Update()
        {
            if (hasAuthority && _canShoot && Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shoot();
            }
        }

        void Shoot()
        {
            CmdShoot(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
            _canShoot = false;
            StartCoroutine(CooldownRoutine());
        }

        [Command]
        private void CmdShoot(Vector3 rayVector)
        {
            Ray ray = Camera.main.ScreenPointToRay(rayVector);
            RaycastHit hitInfo;
            RpcShoot();
            if (Physics.Raycast(ray, out hitInfo, _gunRange))
            {
                GameObject particle = Instantiate(_shootParticle, hitInfo.transform.position, Quaternion.identity);
                NetworkServer.Spawn(particle);
                Destroy(particle, 2);
                if (hitInfo.transform.TryGetComponent(out IDamageable interactable))
                {
                    interactable.Damage(_damage);
                }
            }
        }

        [ClientRpc]
        private void RpcShoot()
        {
            _shootSound.Play();
        }

        IEnumerator CooldownRoutine()
        {
            yield return new WaitForSeconds(_cooldown);
            _canShoot = true;
        }
    }
}