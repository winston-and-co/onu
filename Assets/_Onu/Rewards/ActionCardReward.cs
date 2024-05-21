using ActionCards;

public class ActionCardReward : AbstractReward
{
    AbstractActionCard actionCard;

    public static ActionCardReward New()
    {
        AbstractActionCard ac = ActionCardFactory.MakeRandom();
        ac.gameObject.SetActive(false);
        ActionCardReward reward = New<ActionCardReward>(ac.Name, ac.SpriteName) as ActionCardReward;
        reward.actionCard = ac;
        reward.Tooltips.Copy(ac.Tooltips);
        return reward;
    }

    public override void Collect()
    {
        PlayerData.GetInstance().AddActionCard(actionCard);
    }
}