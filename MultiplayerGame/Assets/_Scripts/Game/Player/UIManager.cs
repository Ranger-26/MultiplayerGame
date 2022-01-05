using Game.GameLogic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Player
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private Text _healthText;

        [SerializeField]
        private Text _roleText;

        [SerializeField]
        private Text _gameOverText;

        public void UpdateHealth(int newHealth)
        {
            _healthText.text = $"Health: {newHealth}";
        }

        public void UpdateRoleText(Role newRole)
        {
            _roleText.text = $"You are a {newRole}";
            _roleText.gameObject.SetActive(true);
        }

        public void UpdateGameOverText(string text)
        {
            _gameOverText.text = text;
            _gameOverText.gameObject.SetActive(true);
        }

        public void ResetUI()
        {
            _roleText.gameObject.SetActive(false);
            _gameOverText.gameObject.SetActive(false);
        }
    }
}