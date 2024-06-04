public class ActionCardReward : AbstractReward
{
    int actionCardId;

    public static ActionCardReward New()
    {
        (var ac, int id) = ActionCardFactory.MakeRandom();
        ac.gameObject.SetActive(false);
        ActionCardReward reward = New<ActionCardReward>(ac.Name, ac.SpriteName) as ActionCardReward;
        reward.actionCardId = id;
        reward.Tooltips.Copy(ac.Tooltips);
        Destroy(ac.gameObject);
        return reward;
    }

    public override void Collect()
    {
        PlayerData.GetInstance().AddActionCard(actionCardId);
    }
}