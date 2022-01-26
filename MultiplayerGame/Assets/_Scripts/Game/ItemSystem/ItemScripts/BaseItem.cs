using Game.Player;
using UnityEngine;

namespace Game.ItemSystem.ItemScripts
{
    [CreateAssetMenu(fileName = "BaseItem", menuName = "BaseItem/BaseItem")]
    public class BaseItem : ScriptableObject
    {
        public virtual ItemType Id { get; } = ItemType.None;

        public virtual void OnUse(NetworkGamePlayer ply)
        {
           
        }

        public virtual void OnEquipt(NetworkGamePlayer ply)
        {

        }

        public virtual void OnDequipt(NetworkGamePlayer ply)
        {

        }
    }
}