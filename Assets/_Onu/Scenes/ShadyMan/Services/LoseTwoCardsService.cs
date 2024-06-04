using System.Collections.Generic;
using Cards;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Player removes 2 cards from their deck
/// </summary>
public class LoseTwoCardsService : AbstractShadyManService
{
    public override bool ConditionMet()
    {
        return true;
    }

    DeckMultiSelect multiSelect;
    public override void OnPointerClick(PointerEventData _)
    {
        multiSelect = PrefabHelper.GetInstance().InstantiatePrefab(PrefabType.UI_DeckMultiSelect).GetComponent<DeckMultiSelect>();
        multiSelect.transform.SetParent(FindObjectOfType<Canvas>().transform, false);
        multiSelect.MinSelection = 2;
        multiSelect.MaxSelection = 2;
        multiSelect.ConfirmedSelection.AddListener(OnConfirmedSelection);
        multiSelect.Show();
    }

    void OnConfirmedSelection(List<AbstractCard> cards)
    {
        var deck = PlayerData.GetInstance().Player.deck;
        for (int i = 0; i < cards.Count; i++)
        {
            deck.RemovePermanently(cards[i]);
        }
        multiSelect.Hide();
        Destroy(multiSelect.gameObject);
        base.Done();
    }
}
