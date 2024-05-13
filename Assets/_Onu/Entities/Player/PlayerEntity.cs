using UnityEngine;

public class PlayerEntity : AbstractEntity
{
    public static PlayerEntity New(string playerName, DeckType deckType, int maxHP, int maxMana, int startingHandSize)
    {
        PlayerEntity player = New(
            name: "Player",
            entityName: playerName,
            entityType: typeof(PlayerEntity),
            maxHP: maxHP,
            maxMana: maxMana,
            startingHandSize: startingHandSize,
            isPlayer: true
        ) as PlayerEntity;
        // DECK SETUP
        Deck deck = DeckFactory.MakeDeck(deckType, player);
        deck.transform.SetPositionAndRotation(new Vector2(5.55f, -3.48f), Quaternion.identity);
        // HAND SETUP
        Hand hand = player.hand;
        hand.transform.position = new Vector2(-2.5f, -4.17f);
        CardHandLayout layout = hand.GetComponent<CardHandLayout>();
        layout.spacing = 1.0f;
        return player;
    }
}