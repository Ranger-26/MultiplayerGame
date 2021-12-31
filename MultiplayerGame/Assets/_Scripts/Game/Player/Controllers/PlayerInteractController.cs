using UnityEngine;

namespace Game.Player.Controllers
{
    public class PlayerInteractController : MonoBehaviour
    {
        [SerializeField] 
        private float interactionDistance = 25;

        private NetworkGamePlayer _playerMain;

        private void Start()
        {
            GetComponent<NetworkGamePlayer>();
        }

        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Checking for interactable...");
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo, interactionDistance))
                {
                    Debug.Log("Hit something!");
                    if (hitInfo.transform.TryGetComponent(out IInteractable interactable))
                    {
                        Debug.Log("Interactable Found!");
                        interactable.OnInteract(_playerMain);
                    }
                }
            }
        }
    }
}