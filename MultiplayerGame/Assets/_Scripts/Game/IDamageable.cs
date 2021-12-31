using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public interface IDamageable
    {
       public void Damage(int damage);   
    }
}