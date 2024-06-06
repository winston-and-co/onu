using System;

namespace ActionCards
{
    public class Generate2 : AbstractActionCard
    {
        public static int Id => 7;
        public override string Name => "Generate 2";
        public override string Description => "Creates 2 random Action Cards.";
        public override string SpriteName => "alpha_art_card_front";

        public static Generate2 New() => New<Generate2>();

        public override bool IsUsable()
        {
            return true;
        }

        public override void Use(Action resolve)
        {
            var pd = PlayerData.GetInstance();
            pd.AddActionCard(ActionCardFactory.GetRandomId());
            pd.AddActionCard(ActionCardFactory.GetRandomId());
            resolve();
        }
    }
}