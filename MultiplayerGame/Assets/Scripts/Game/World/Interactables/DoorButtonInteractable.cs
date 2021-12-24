using System;
using Game.Player;
using UnityEngine;

namespace Game.World.Interactables
{
    public class DoorButtonInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private Door.Door door;
        
        public void OnInteract(NetworkGamePlayer player)
        {
            door.CmdChangeDoorState();
        }

        public void Highlight()
        {
            
        }
    }
}