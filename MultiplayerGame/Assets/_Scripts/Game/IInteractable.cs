using Game.Player;

namespace Game
{
    public interface IInteractable
    {
        void OnInteract(NetworkGamePlayer player);

        void Highlight();
    }
}