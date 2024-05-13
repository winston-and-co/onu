using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards;

public class SimpleAI : AbstractPattern
{
    [SerializeField] int stupidity;

    float ThinkDuration() => Random.Range(1.00f, 3.00f);

    protected override IEnumerator DoTurn()
    {
        bool continueTurn;
        do
        {
            yield return new WaitForSeconds(ThinkDuration());
            continueTurn = PickCardToPlay();
        } while (continueTurn);
    }

    bool PickCardToPlay()
    {
        List<AbstractCard> legalCards = new();
        foreach (var c in entity.hand.hand)
        {
            if (c.IsPlayable())
            {
                legalCards.Add(c);
            }
        }
        if (legalCards.Count > 0)
        {
            int chancePickCard = Random.Range(1, 100);
            if (chancePickCard > stupidity)
            {
                var cardPicked = legalCards[Random.Range(0, legalCards.Count)];
                BattleEventBus.getInstance().cardTryPlayedEvent.Invoke(entity, cardPicked);
                return true;
            }
        }
        return false;
    }
}