using Game.Player;

namespace Game.World
{
    public interface IInteractable
    {
        void OnInteract(NetworkGamePlayer ply);
    }
}