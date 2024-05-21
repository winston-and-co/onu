using ActionCards;

public class ActionCardReward : AbstractReward
{
    string actionCardName;

    public static ActionCardReward New()
    {
        AbstractActionCard ac = ActionCardFactory.MakeRandom();
        ActionCardReward reward = New<ActionCardReward>(ac.Name, ac.SpriteName) as ActionCardReward;
        reward.actionCardName = ac.Name;
        Destroy(ac.gameObject);
        return reward;
    }

    public override void Collect()
    {
        PlayerData.GetInstance().AddActionCard(actionCardName);
    }
}