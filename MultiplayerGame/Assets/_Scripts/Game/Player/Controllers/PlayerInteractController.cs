using Game.World;
using Mirror;
using UnityEngine;

namespace Game.Player.Controllers
{
    public class PlayerInteractController : NetworkBehaviour
    {
        [SerializeField] 
        private float interactionDistance = 25;

        private NetworkGamePlayer _playerMain;

        private IInteractable _curInteractable;
        private void Start()
        {
            GetComponent<NetworkGamePlayer>();
        }

        private void Update()
        {
            if (!hasAuthority) return;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, interactionDistance))
            {
                if (hitInfo.transform.TryGetComponent(out IInteractable interactable))
                {
                    if (_curInteractable != null && _curInteractable == interactable)
                    {
                        _curInteractable.Highlight();
                    }
                    else
                    {
                        _curInteractable = interactable;
                        _curInteractable.Highlight();
                    }
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        interactable.OnInteract(_playerMain);
                    }
                }
                else
                {
                    if (_curInteractable != null)
                    {
                        _curInteractable.UnHighlight();
                        _curInteractable = null;
                    }
                }
                
            }
        }
    }
}