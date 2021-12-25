using Game.Player;

namespace Game.World.ItemSystem
{
    public class Consumable : ItemBase
    {
        public virtual int GetHealAmount()
        {
            return 25;
        }

        public override bool OnUse(NetworkGamePlayer ply)
        {
            ply.healthController.CmdDamage(-1*GetHealAmount());
            return true;
        }
    }
}