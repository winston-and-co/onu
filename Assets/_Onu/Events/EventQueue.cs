using System.Collections.Generic;
using Cards;
using ActionCards;

using EventInvoker = System.Action;

public class EventQueue
{
    private static EventQueue INSTANCE;
    public static EventQueue GetInstance()
    {
        return INSTANCE ?? (INSTANCE = new EventQueue());
    }

    readonly List<EventInvoker> invokers = new();

    bool running = false;

    public void AddToFront(EventInvoker e)
    {
        invokers.Insert(0, e);
        if (!running) StartInvoking();
    }

    public void AddToBack(EventInvoker e)
    {
        invokers.Add(e);
        if (!running) StartInvoking();
    }

    public void Clear()
    {
        if (!running) invokers.Clear();
    }

    void StartInvoking()
    {
        running = true;
        while (invokers.Count > 0)
        {
            EventInvoker e = invokers[0];
            e();
            invokers.Remove(e);
        }
        running = false;
    }

    public CardTryPlayedEvent cardTryPlayedEvent = new();
    public CardPlayedEvent cardPlayedEvent = new();
    public CardDrawEvent cardDrawEvent = new();
    public CardNoDrawEvent cardNoDrawEvent = new();
    public CardTryDrawEvent cardTryDrawEvent = new();

    public EntityHealthChangedEvent entityHealthChangedEvent = new();
    public EntityMaxHealthChanged entityMaxHealthChangedEvent = new();
    public EntityManaChangedEvent entityManaChangedEvent = new();
    public EntityMaxManaChangedEvent entityMaxManaChangedEvent = new();
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
public class CardTryPlayedEvent : GameEvent<AbstractEntity, AbstractCard> { }
public class CardPlayedEvent : GameEvent<AbstractEntity, AbstractCard> { }
public class CardDrawEvent : GameEvent<AbstractEntity, AbstractCard> { }
public class CardNoDrawEvent : GameEvent<AbstractEntity, AbstractCard> { }
public class CardTryDrawEvent : GameEvent<AbstractEntity, AbstractCard> { }

// Game flow
public class StartBattleEvent : GameEvent<GameMaster> { }
public class StartTurnEvent : GameEvent<AbstractEntity> { }
public class EndTurnEvent : GameEvent<AbstractEntity> { }
public class TryEndTurnEvent : GameEvent<AbstractEntity> { }
public class EndBattleEvent : GameEvent<GameMaster> { }

// Stats
public class EntityHealthChangedEvent : GameEvent<AbstractEntity, int> { }
public class EntityMaxHealthChanged : GameEvent<AbstractEntity, int> { }
public class EntityManaChangedEvent : GameEvent<AbstractEntity, int> { }
public class EntityMaxManaChangedEvent : GameEvent<AbstractEntity, int> { }

// Game
public class EntityRefreshEvent : GameEvent<AbstractEntity> { }

// Action Cards
public class ActionCardTryUseEvent : GameEvent<AbstractEntity, AbstractActionCard> { }
public class ActionCardUsedEvent : GameEvent<AbstractEntity, AbstractActionCard> { }
