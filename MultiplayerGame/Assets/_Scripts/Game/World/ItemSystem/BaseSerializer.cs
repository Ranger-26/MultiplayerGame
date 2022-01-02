using Mirror;

namespace Game.World.ItemSystem
{
    public abstract class BaseSerializer
    {
        public abstract ItemType Id { get; }
        
        public abstract void Write(NetworkWriter writer, ItemBase item);
        
        public abstract ItemBase Read(NetworkReader reader, int id);
        
    }
}