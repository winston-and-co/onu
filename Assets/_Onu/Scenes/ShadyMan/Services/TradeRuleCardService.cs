using System;
using System.Linq;
using RuleCards;
using UnityEngine.EventSystems;

public class TradeRuleCardService : AbstractShadyManService
{
    public override bool ConditionMet()
    {
        return PlayerData.GetInstance().Player.gameRulesController.GetCount() > 0;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        var pd = PlayerData.GetInstance();
        var currentRules = pd.Player.gameRulesController.Rules.ToList();
        var rand = new Random();
        var toRemove = currentRules[1 + rand.Next(currentRules.Count - 1)];
        bool res = pd.RemoveRuleCard(toRemove);
        if (res)
        {
            AbstractRuleCard rc = RuleCardFactory.MakeRandom();
            pd.AddRuleCard(rc);
        }
        base.Done();
    }
}