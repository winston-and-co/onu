using RuleCards;

public class RuleCardReward : AbstractReward
{
    AbstractRuleCard ruleCard;

    public static RuleCardReward New()
    {
        var rc = RuleCardFactory.MakeRandom();
        var reward = New<RuleCardReward>(rc.Name, rc.SpriteName) as RuleCardReward;
        reward.ruleCard = rc;
        var temp = rc.ToGameObject();
        reward.Tooltips.Copy(temp.GetComponent<RuleCardUIObject>().Tooltips);
        Destroy(temp);
        return reward;
    }

    public override void Collect()
    {
        PlayerData.GetInstance().AddRuleCard(ruleCard);
    }
}