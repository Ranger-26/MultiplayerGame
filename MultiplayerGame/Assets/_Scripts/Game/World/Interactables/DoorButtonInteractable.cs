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
            GetComponent<Renderer>().material.SetColor("_Color",Color.yellow);
        }

        public void UnHighlight()
        {
            GetComponent<Renderer>().material.SetColor("_Color",Color.red);
        }
    }
}