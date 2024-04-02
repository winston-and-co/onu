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
    public EntityManaSpentEvent entityManaSpentEvent = new EntityManaSpentEvent();
    public StartTurnEvent startTurnEvent = new StartTurnEvent();
    public EndTurnEvent endTurnEvent = new EndTurnEvent();
    public TryEndTurnEvent tryEndTurnEvent = new TryEndTurnEvent();

}

// Game rules
public class CardTryPlayedEvent : UnityEvent<Entity, Card> { }
public class CardPlayedEvent : UnityEvent<Entity, Card> { }
public class CardIllegalEvent : UnityEvent<Entity, Card> { }
public class CardDrawEvent : UnityEvent<Entity, Card> { }
public class CardNoDrawEvent : UnityEvent<Entity, Card> { }
public class CardTryDrawEvent : UnityEvent<Entity, Card> { }

// Game flow

public class StartTurnEvent : UnityEvent<Entity> { }
public class EndTurnEvent : UnityEvent<Entity> { }
public class TryEndTurnEvent : UnityEvent<Entity> { }

// Combat
public class EntityDamageEvent : UnityEvent<Entity, float> { }
public class EntityManaSpentEvent : UnityEvent<Entity, float> { }