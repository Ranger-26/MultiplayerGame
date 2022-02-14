using System.Collections;
using UnityEngine;
using Mirror;
using Assets._Scripts.Game.ItemSystem.Items;
using Game;

namespace Assets._Scripts.Game.Player
{
    public class WeaponManager : NetworkBehaviour
    {
        [SyncVar] public Gun curGun;

        [Command]
        public void CmdFireWeapon(Vector3 rayVector)
        {
            Ray ray = Camera.main.ScreenPointToRay(rayVector);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, curGun.gunRange))
            {
                if (hitInfo.transform.TryGetComponent(out IDamageable interactable))
                {
                    interactable.Damage(curGun.baseDamage);
                }
            }
        }


    }
}