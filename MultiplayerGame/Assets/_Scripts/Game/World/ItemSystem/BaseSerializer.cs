using Mirror;

namespace Game.World.ItemSystem
{
    public abstract class BaseSerializer
    {
        public abstract ItemType Id { get; }
        
        public abstract void Write(NetworkWriter writer, ItemBase item);

        // Token: 0x06001BB7 RID: 7095
        public abstract ItemBase Read(NetworkReader reader, int id);
        
    }
}