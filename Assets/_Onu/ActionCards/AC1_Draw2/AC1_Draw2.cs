using System;

namespace ActionCards
{
    public class Draw2 : AbstractActionCard
    {
        public static int Id => 1;
        public override string Name => "Draw 2";
        public override string Description => "Draw 2 cards";
        public override string SpriteName => "AC1_Draw2";

        public static Draw2 New() => New<Draw2>();

        public override void Use(Action resolve)
        {
            var player = PlayerData.GetInstance().Player;
            player.Draw();
            player.Draw();
            resolve();
        }
    }
}