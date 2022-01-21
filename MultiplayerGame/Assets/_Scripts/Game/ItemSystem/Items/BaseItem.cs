using Game.Player;
using UnityEditor;
using UnityEngine;

namespace Assets._Scripts.Game.ItemSystem.Items
{
    public class BaseItem : ScriptableObject
    {
        public virtual ItemType Id { get; }

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