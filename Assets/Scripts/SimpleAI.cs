using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAI : MonoBehaviour
{
    [SerializeField] Entity entity;
    [SerializeField] int stupidity;

    public void Awake()
    {
        entity = GetComponent<Entity>();
        var bus = BattleEventBus.getInstance();
        bus.startTurnEvent.AddListener(OnTurnStart);
    }

    void OnTurnStart(Entity e)
    {
        if (entity != e) return;
        StartCoroutine(DoTurn());
    }

    float ThinkDuration()
    {
        return Random.Range(1.00f, 5.00f);
    }

    IEnumerator DoTurn()
    {
        bool continueTurn;
        do
        {
            yield return new WaitForSeconds(ThinkDuration());
            continueTurn = PickCardToPlay();
        } while (continueTurn);
        BattleEventBus.getInstance().tryEndTurnEvent.Invoke(entity);
    }

    bool PickCardToPlay()
    {
        List<Playable> legalCards = new();
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