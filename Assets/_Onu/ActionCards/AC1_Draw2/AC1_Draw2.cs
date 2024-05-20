using System;

namespace ActionCards
{
    public class Draw2 : AbstractActionCard
    {
        public override int Id => 1;
        public override string Name => "Draw2";

        public override bool IsUsable()
        {
            var gm = GameMaster.GetInstance();
            if (gm.current_turn_entity == gm.player)
            {
                return true;
            }
            return false;
        }

        public override void TryUse()
        {
            throw new NotImplementedException();
        }

        public override void Use(Action onResolved)
        {
            throw new NotImplementedException();
        }
    }
}