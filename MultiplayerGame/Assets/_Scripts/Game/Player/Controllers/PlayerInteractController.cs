using UnityEngine;

namespace Game.Player.Controllers
{
    public class PlayerInteractController : MonoBehaviour
    {
        [SerializeField] 
        private float interactionDistance = 25;

        private NetworkGamePlayer _playerMain;

        private IInteractable curInteractable;
        private void Start()
        {
            GetComponent<NetworkGamePlayer>();
        }

        private void FixedUpdate()
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, interactionDistance))
            {
                if (hitInfo.transform.TryGetComponent(out IInteractable interactable))
                {
                    curInteractable = interactable;
                    curInteractable.Highlight();
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        interactable.OnInteract(GetComponent<NetworkGamePlayer>());
                    }
                }
                else
                {
                    curInteractable?.UnHighlight();
                    curInteractable = null;
                }
            }
            
        }
    }
}