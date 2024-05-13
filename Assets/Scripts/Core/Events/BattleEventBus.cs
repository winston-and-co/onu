using System.Collections;
using System.Collections.Generic;
using Cards;
using ActionCards;
using UnityEngine;
using UnityEngine.Events;

public class BattleEventBus
{
    private static BattleEventBus EVENTBUS;

    public static BattleEventBus getInstance()
    {
        return EVENTBUS ?? (EVENTBUS = new BattleEventBus());
    }

    public CardTryPlayedEvent cardTryPlayedEvent = new();
    public CardPlayedEvent cardPlayedEvent = new();
    public AfterCardPlayedEvent afterCardPlayedEvent = new();
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

    public ActionCardTryUseEvent actionCardTryUseEvent = new();
    public ActionCardUsedEvent actionCardUsedEvent = new();

    public StartBattleEvent startBattleEvent = new();
    public StartTurnEvent startTurnEvent = new();
    public EndTurnEvent endTurnEvent = new();
    public TryEndTurnEvent tryEndTurnEvent = new();
    public EndBattleEvent endBattleEvent = new();
}

// Game rules
public class CardTryPlayedEvent : UnityEvent<AbstractEntity, AbstractCard> { }
public class CardPlayedEvent : UnityEvent<AbstractEntity, AbstractCard> { }
public class AfterCardPlayedEvent : UnityEvent<AbstractEntity, AbstractCard> { }
public class CardIllegalEvent : UnityEvent<AbstractEntity, AbstractCard> { }
public class CardDrawEvent : UnityEvent<AbstractEntity, AbstractCard> { }
public class CardNoDrawEvent : UnityEvent<AbstractEntity, AbstractCard> { }
public class CardTryDrawEvent : UnityEvent<AbstractEntity, AbstractCard> { }

// Game flow
public class StartBattleEvent : UnityEvent<GameMaster> { }
public class StartTurnEvent : UnityEvent<AbstractEntity> { }
public class EndTurnEvent : UnityEvent<AbstractEntity> { }
public class TryEndTurnEvent : UnityEvent<AbstractEntity> { }
public class EndBattleEvent : UnityEvent<GameMaster> { }

// Combat
public class EntityHealthChangedEvent : UnityEvent<AbstractEntity, int> { }
public class EntityDamageEvent : UnityEvent<AbstractEntity, int> { }
public class EntityHealEvent : UnityEvent<AbstractEntity, int> { }
public class EntityManaChangedEvent : UnityEvent<AbstractEntity, int> { }
public class EntityManaSpentEvent : UnityEvent<AbstractEntity, int> { }
public class EntityRefreshEvent : UnityEvent<AbstractEntity> { }

// Action Cards
public class ActionCardTryUseEvent : UnityEvent<AbstractEntity, AbstractActionCard> { }
public class ActionCardUsedEvent : UnityEvent<AbstractEntity, AbstractActionCard> { }