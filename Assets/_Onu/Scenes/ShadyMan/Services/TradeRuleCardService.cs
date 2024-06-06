using System;
using System.Linq;
using RuleCards;
using UnityEngine.EventSystems;

public class TradeRuleCardService : AbstractShadyManService
{
    public override bool ConditionMet()
    {
        return PlayerData.GetInstance().Player.gameRulesController.Rules.Count > 0;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        var pd = PlayerData.GetInstance();
        var currentRules = pd.Player.gameRulesController.Rules.ToList();
        var rand = new Random();
        var toRemove = currentRules[rand.Next(currentRules.Count)];
        pd.RemoveRuleCard(toRemove);
        AbstractRuleCard rc = RuleCardFactory.MakeRandom();
        pd.AddRuleCard(rc);
        base.Done();
    }
}