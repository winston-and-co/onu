using System.Collections.Generic;
using Cards;
using UnityEngine;

public class FlushCards : MonoBehaviour
{
    [SerializeField] int num_lost = 2;

    DeckMultiSelect multiSelect;
    public void Flush()
    {
        multiSelect = PrefabLoader.GetInstance().InstantiatePrefab(PrefabType.UI_DeckMultiSelect).GetComponent<DeckMultiSelect>();
        multiSelect.transform.SetParent(FindObjectOfType<Canvas>().transform, false);
        multiSelect.MinSelection = num_lost;
        multiSelect.MaxSelection = num_lost;
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
        GetComponent<BathroomEvent>().Done();
    }

    public void DontFlush()
    {
        GetComponent<BathroomEvent>().Done();
    }
}
