using Lobby;
using Mirror;
using UnityEngine;

namespace MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private NetworkManagerLobby networkManager;

        [Header("UI")] [SerializeField] private GameObject landingPagePanel;

        public void HostLobby()
        {
            networkManager.StartHost();
            
            landingPagePanel.SetActive(false);
        }
    }
}