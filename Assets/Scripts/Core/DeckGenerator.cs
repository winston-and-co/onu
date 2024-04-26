using System.Collections.Generic;
using UnityEngine;

public enum DeckType
{
    Standard, // 2 of each 0-9 for RBGY
}

public class DeckGenerator : MonoBehaviour
{
    static DeckGenerator Instance;
    public static DeckGenerator GetInstance() => Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] GameObject playerDeckPrefab;
    [SerializeField] GameObject playerCardPrefab;
    [SerializeField] GameObject enemyDeckPrefab;
    [SerializeField] GameObject enemyCardPrefab;

    public Deck Generate(DeckType type, Entity e)
    {
        var deckPrefab = e.isPlayer ? playerDeckPrefab : enemyDeckPrefab;
        var cardPrefab = e.isPlayer ? playerCardPrefab : enemyCardPrefab;
        var newDeck = Instantiate(deckPrefab).GetComponent<Deck>();
        if (newDeck == null) throw new System.Exception("Deck prefab missing Deck script component");
        newDeck.e = e;
        List<Card> cards = type switch
        {
            DeckType.Standard => Standard(newDeck, cardPrefab),
            _ => new(),
        };
        newDeck.Populate(cards);
        return newDeck;
    }

    List<Card> Standard(Deck deck, GameObject cardPrefab)
    {
        Color[] colors = new Color[] {
            CardColors.Red,
            CardColors.Blue,
            CardColors.Green,
            CardColors.Yellow,
        };
        deck.colors = colors;
        List<Card> cards = new();
        for (int sets = 0; sets < 2; sets++)
        {
            for (int value = 0; value < 10; value++)
            {
                for (int colorIdx = 0; colorIdx < colors.Length; colorIdx++)
                {
                    Card newCard = Instantiate(cardPrefab).GetComponent<Card>();
                    if (newCard == null) throw new System.Exception("Card prefab missing Card script component");
                    newCard.Value = new NullableInt { Value = value, IsNull = false };
                    newCard.Color = colors[colorIdx];

                    newCard.transform.SetParent(deck.transform, false);
                    newCard.entity = deck.e;
                    newCard.gameObject.SetActive(false);

                    cards.Add(newCard);
                }
            }
        }
        return cards;
    }
}