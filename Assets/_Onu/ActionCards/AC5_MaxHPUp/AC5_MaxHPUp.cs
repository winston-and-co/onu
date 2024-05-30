using System;

namespace ActionCards
{
    public class MaxHPUp : AbstractActionCard
    {
        public static int Id => 5;
        public override string Name => "Max HP Up";
        public override string Description => "Gain 5 Max HP";
        public override string SpriteName => "alpha_art_card_front";

        public static MaxHPUp New() => New<MaxHPUp>();

        public override bool IsUsable()
        {
            return true;
        }

        public override void Use(Action resolve)
        {
            var p = PlayerData.GetInstance().Player;
            p.ChangeMaxHP(5);
            resolve();
        }
    }
}