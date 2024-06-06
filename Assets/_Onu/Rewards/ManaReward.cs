using UnityEngine;

public class ManaReward : AbstractReward
{
    int value;

    public static ManaReward New()
    {
        int size = Random.Range(0, 2);
        ManaReward reward;
        reward = New<ManaReward>("Small Mana Potion", "small_mana_potion") as ManaReward;
        reward.value = Random.Range(3 * size + 1, 3 * size + 3);
        reward.Tooltips.Add(new()
        {
            Title = "Small Mana Potion",
            Body = $"You aren't sure if it's safe to drink. Increases your max mana by {reward.value}.",
        });
        return reward;
    }

    public override void Collect()
    {
        PlayerData.GetInstance().Player.ChangeMaxMana(value);
    }
}