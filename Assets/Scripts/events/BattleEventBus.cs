using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleEventBus 
{
    private static BattleEventBus EVENTBUS;

    public static BattleEventBus getInstance()
    {
        return EVENTBUS ?? (EVENTBUS = new BattleEventBus());
    }

    public CardTryPlayedEvent tryPlayEvent = new CardTryPlayedEvent();
    public CardPlayedEvent cardPlayedEvent = new CardPlayedEvent();
    public CardIllegalEvent cardIllegalEvent = new CardIllegalEvent();
    public CardDrawEvent cardDrawEvent = new CardDrawEvent();
    public CardNoDrawEvent cardNoDrawEvent = new CardNoDrawEvent();
    public CardTryDrawEvent cardTryDrawEvent = new CardTryDrawEvent();
    public EntityDamageEvent entityDamageEvent = new EntityDamageEvent();

}

public class CardTryPlayedEvent : UnityEvent<Entity, Card> { }
public class CardPlayedEvent : UnityEvent<Entity, Card> { }
public class CardIllegalEvent : UnityEvent<Entity, Card> { }

public class CardDrawEvent : UnityEvent<Entity, Card> { }
public class CardNoDrawEvent : UnityEvent<Entity, Card> { }
public class CardTryDrawEvent : UnityEvent<Entity, Card> { }
public class EntityDamageEvent : UnityEvent<Entity, float> { }