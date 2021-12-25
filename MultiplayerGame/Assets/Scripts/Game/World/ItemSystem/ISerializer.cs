using Mirror;

namespace Game.World.ItemSystem
{
    public interface ISerializer
    {
        public void Write(NetworkWriter writer, ItemBase item);
        
        public ItemBase Read(NetworkReader reader, ItemType id);
    }
}