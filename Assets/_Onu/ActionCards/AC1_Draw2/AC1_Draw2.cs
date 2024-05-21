using System;

namespace ActionCards
{
    public class Draw2 : AbstractActionCard
    {
        public override int Id => 1;
        public override string Name => "Draw 2";
        public override string Description => "Draw 2 cards";
        public override string SpriteName => "AC1_Draw2";

        public static Draw2 New()
        {
            var draw2 = New<Draw2>(true) as Draw2;
            return draw2;
        }

        public override bool IsUsable()
        {
            var gm = GameMaster.GetInstance();
            if (gm.CurrentEntity == gm.Player)
            {
                return true;
            }
            return false;
        }

        public override void Use(Action resolve)
        {
            var player = PlayerData.GetInstance().Player;
            player.Draw();
            player.Draw();
            resolve();
        }
    }
}