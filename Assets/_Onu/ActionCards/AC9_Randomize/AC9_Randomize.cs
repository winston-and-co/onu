using System;

namespace ActionCards
{
    public class Randomize : AbstractActionCard
    {
        public static int Id => 9;
        public override string Name => "Randomize";
        public override string Description => "Randomize the Value and Color of each card in your hand";
        public override string SpriteName => "alpha_art_card_front";

        public static Randomize New() => New<Randomize>();

        public override void Use(Action resolve)
        {
            var rand = new Random();
            var player = PlayerData.GetInstance().Player;
            for (int i = 0; i < player.hand.GetCardCount(); i++)
            {
                var c = player.hand.GetCard(i);
                c.Value = rand.Next(10);
                c.Color = rand.Next(4) switch
                {
                    0 => CardColor.Red,
                    1 => CardColor.Blue,
                    2 => CardColor.Green,
                    3 => CardColor.Yellow,
                    _ => throw new Exception(),
                };
            }
            resolve();
        }
    }
}