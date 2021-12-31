using System.Collections;
using Mirror;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.World.Door
{
    public class Door : NetworkBehaviour
    {
        [SyncVar] public DoorState curState;

        [SerializeField]
        private NetworkAnimator animator;

        public override void OnStartServer()
        {
            base.OnStartServer();
            animator = GetComponent<NetworkAnimator>();
        }

        [Command(requiresAuthority = false)]
        public void CmdChangeDoorState()
        {
            if (animator.animator.IsInTransition(0))
            {
                Debug.Log("Returning out of animation");
                return;
            }
            
            Debug.Log($"Calling CmdChangeDoorState....has a state of {curState}");
            if (curState == DoorState.Open)
            {
                ServerCloseDoor();
                return;
            }
            
            if (curState == DoorState.Closed)
            {
                ServerOpenDoor();
            }
        }

        [Server]
        private void ServerOpenDoor()
        {
            animator.SetTrigger("DoorOpen");
            curState = DoorState.Open;
        }

        [Server]
        private void ServerCloseDoor()
        {
            animator.SetTrigger("DoorClose");
            curState = DoorState.Closed;
        }
    }
}