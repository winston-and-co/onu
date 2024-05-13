using UnityEngine;

public class EnemyEntity : AbstractEntity
{
    public static EnemyEntity New(string enemyName, DeckType deckType, int maxHP, int maxMana, int startingHandSize, System.Type patternType, string spriteName)
    {
        EnemyEntity enemy = New(
            name: "Enemy",
            entityName: enemyName,
            entityType: typeof(EnemyEntity),
            maxHP: maxHP,
            maxMana: maxMana,
            startingHandSize: startingHandSize,
            isPlayer: false
        ) as EnemyEntity;
        // DECK SETUP
        Deck deck = DeckFactory.MakeDeck(deckType, enemy);
        deck.transform.SetPositionAndRotation(new Vector2(-3.91f, -1.44f), Quaternion.Euler(0, 0, 180f));
        deck.transform.localScale = new Vector3(0.2f, 0.2f, 1);
        // HAND SETUP
        Hand hand = enemy.hand;
        hand.transform.position = new Vector2(-1.19f, -0.5f);
        CardHandLayout layout = hand.GetComponent<CardHandLayout>();
        layout.spacing = 0.5f;
        // PATTERN
        if (patternType != null) enemy.gameObject.AddComponent(patternType);
        // SPRITE
        GameObject spriteObject = new("Sprite");
        spriteObject.transform.position = new Vector2(0, 1.5f);
        spriteObject.transform.SetParent(enemy.transform);
        SpriteRenderer sr = spriteObject.AddComponent<SpriteRenderer>();
        sr.sprite = SpriteLoader.LoadSprite(spriteName);
        sr.sortingOrder = -2;
        return enemy;
    }
}