using ActionCards;

public class ActionCardReward : AbstractReward
{
    public AbstractActionCard ActionCard;

    public static ActionCardReward New()
    {
        AbstractActionCard ac = ActionCardFactory.MakeRandom();
        ActionCardReward reward = New(typeof(ActionCardReward), ac.Name, ac.SpriteName) as ActionCardReward;
        reward.ActionCard = ac;
        print("here");
        Destroy(ac.gameObject);
        return reward;
    }

    public override void Collect()
    {
        print(ActionCard.Name);
    }
}