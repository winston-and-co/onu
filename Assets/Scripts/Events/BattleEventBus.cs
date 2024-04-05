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

    public CardTryPlayedEvent tryPlayEvent = new();
    public CardPlayedEvent cardPlayedEvent = new();
    public CardIllegalEvent cardIllegalEvent = new();
    public CardDrawEvent cardDrawEvent = new();
    public CardNoDrawEvent cardNoDrawEvent = new();
    public CardTryDrawEvent cardTryDrawEvent = new();
    public EntityDamageEvent entityDamageEvent = new();
    public EntityHealEvent entityHealEvent = new();
    public EntityHealthChangedEvent entityHealthChangedEvent = new();
    public EntityManaSpentEvent entityManaSpentEvent = new();
    public EntityManaChangedEvent entityManaChangedEvent = new();
    public EntityRefreshEvent entityRefreshEvent = new();

    public StartBattleEvent startBattleEvent = new();
    public StartTurnEvent startTurnEvent = new();
    public EndTurnEvent endTurnEvent = new();
    public TryEndTurnEvent tryEndTurnEvent = new();
    public EndBattleEvent endBattleEvent = new();
}

// Game rules
public class CardTryPlayedEvent : UnityEvent<Entity, Card> { }
public class CardPlayedEvent : UnityEvent<Entity, Card> { }
public class CardIllegalEvent : UnityEvent<Entity, Card> { }
public class CardDrawEvent : UnityEvent<Entity, Card> { }
public class CardNoDrawEvent : UnityEvent<Entity, Card> { }
public class CardTryDrawEvent : UnityEvent<Entity, Card> { }

// Game flow
public class StartBattleEvent : UnityEvent<GameMaster> { }
public class StartTurnEvent : UnityEvent<Entity> { }
public class EndTurnEvent : UnityEvent<Entity> { }
public class TryEndTurnEvent : UnityEvent<Entity> { }
public class EndBattleEvent : UnityEvent<GameMaster> { }

// Combat
public class EntityHealthChangedEvent : UnityEvent<Entity, int> { }
public class EntityDamageEvent : UnityEvent<Entity, int> { }
public class EntityHealEvent : UnityEvent<Entity, int> { }
public class EntityManaChangedEvent : UnityEvent<Entity, int> { }
public class EntityManaSpentEvent : UnityEvent<Entity, int> { }
public class EntityRefreshEvent : UnityEvent<Entity> { }
