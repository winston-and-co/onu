using System;

namespace ActionCards
{
    public class Skip : AbstractActionCard
    {
        public static int Id => 2;
        public override string Name => "Skip";
        public override string Description => "Skips the opponent's next turn.";
        public override string SpriteName => "alpha_art_card_front";

        public static Skip New() => New<Skip>();

        public override void Use(Action resolve)
        {
            GameMaster.GetInstance().Enemy.SkipTurn();
            resolve();
        }
    }
}