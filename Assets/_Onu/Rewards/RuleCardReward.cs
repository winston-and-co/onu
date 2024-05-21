using RuleCards;

public class RuleCardReward : AbstractReward
{
    AbstractRuleCard ruleCard;

    public static RuleCardReward New()
    {
        var rc = RuleCardFactory.MakeRandom();
        rc.gameObject.SetActive(false);
        var reward = New<RuleCardReward>(rc.Name, rc.SpriteName) as RuleCardReward;
        reward.ruleCard = rc;
        reward.Tooltips.Copy(rc.Tooltips);
        return reward;
    }

    public override void Collect()
    {
        PlayerData.GetInstance().AddRuleCard(ruleCard);
    }
}