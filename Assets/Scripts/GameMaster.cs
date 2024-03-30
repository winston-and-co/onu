using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameMaster : MonoBehaviour
{
    [SerializeField] Deck playerDeck;
    [SerializeField] Deck enemyDeck;
    [SerializeField] Pile discard;
    [SerializeField] Hand playerHand;

    [SerializeField] Hand enemyHand;
    [SerializeField] Entity player;
    [SerializeField] Entity enemy;

    public CardTryPlayedEvent tryPlayEvent = new CardTryPlayedEvent();
    public CardPlayedEvent cardPlayedEvent = new CardPlayedEvent();
    public CardIllegalEvent cardIllegalEvent = new CardIllegalEvent();
    public PlayerCardDrawEvent playerCardDrawEvent = new PlayerCardDrawEvent();
    public PlayerCardDrawEvent playerCardNoDrawEvent = new PlayerCardDrawEvent();
    public PlayerCardTryDrawEvent playerCardTryDrawEvent = new PlayerCardTryDrawEvent();
    public EntityDamageEvent entityDamageEvent = new EntityDamageEvent();

    // Start is called before the first frame update
    void Start()
    {
        tryPlayEvent.AddListener(TryPlay);
        playerCardTryDrawEvent.AddListener(OnPlayerTryDraw);
    }
    void StartTurn()
    {

    }

    void TryPlay(Card c)
    {
        // TODO: real life logic
        // successful:
        if (true)
        {
            PlayCard(c,true);
        }
        else
        {
            cardIllegalEvent.Invoke(c);
        }
    }

    void PlayCard(Card c, bool wasPlayer)
    {
        if(wasPlayer)
        {
            float dmg = c.value;
            enemy.Damage(dmg);
        }
        cardPlayedEvent.Invoke(c);
    }

    void OnPlayerTryDraw(Card c)
    {
        if(playerHand.GetCardCount() < 7) // logic
        {
            playerCardDrawEvent.Invoke(c);
        } else
        {
            playerCardNoDrawEvent.Invoke(c);
        }
    }

    void OnPlayerDraw(Card c)
    {
        player.SpendMana(c.value);
    }
}

public class CardTryPlayedEvent : UnityEvent<Card> { }
public class CardPlayedEvent : UnityEvent<Card> { }
public class CardIllegalEvent : UnityEvent<Card> { }

public class PlayerCardDrawEvent : UnityEvent<Card> { }
public class PlayerCardNoDrawEvent : UnityEvent<Card> { }
public class PlayerCardTryDrawEvent : UnityEvent<Card> { }
public class EntityDamageEvent : UnityEvent<Entity, float> { }