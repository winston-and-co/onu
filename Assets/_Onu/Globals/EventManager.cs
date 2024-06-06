using System.Collections.Generic;
using Cards;
using ActionCards;

using EventInvoker = System.Action;
using UnityEngine.Events;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    static EventManager Instance;
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
    void Update()
    {
        _queue.InvokeNext();
    }

    readonly GameEventQueue _queue = new();
    public static GameEventQueue Queue { get => Instance._queue; }

    public static GameEvent<AbstractEntity, AbstractCard> cardPlayedEvent = new();
    public static GameEvent<AbstractEntity, AbstractCard> cardDrawnEvent = new();
    public static GameEvent<AbstractEntity, Deck> deckShuffledEvent = new();

    public static GameEvent<AbstractEntity, int> entityHealthChangedEvent = new();
    public static GameEvent<AbstractEntity, int> entityMaxHealthChangedEvent = new();
    public static GameEvent<AbstractEntity, int> entityManaChangedEvent = new();
    public static GameEvent<AbstractEntity, int> entityMaxManaChangedEvent = new();
    public static GameEvent<AbstractEntity> entityRefreshEvent = new();

    public static GameEvent<AbstractActionCard> actionCardUsedEvent = new();

    public static GameEvent startGameEvent = new();
    public static GameEvent beforeBattleStartEvent = new();
    public static GameEvent startedBattleEvent = new();
    public static GameEvent<AbstractEntity> startedTurnEvent = new();
    public static GameEvent<AbstractEntity> tryEndTurnEvent = new();
    public static GameEvent<AbstractEntity> endedTurnEvent = new();
    public static GameEvent<AbstractEntity> skippedTurnEvent = new();
    public static GameEvent endedBattleEvent = new();
    public static GameEvent endGameEvent = new();
}

public class GameEventQueue
{
    public readonly List<EventInvoker> Items = new();

    bool running = false;

    public void AddToFront(EventInvoker e)
    {
        Items.Insert(0, e);
    }

    public void AddToBack(EventInvoker e)
    {
        Items.Add(e);
    }

    public void Clear()
    {
        if (!running) Items.Clear();
    }

    public void InvokeNext()
    {
        if (Items.Count == 0) return;
        EventInvoker e = Items[0];
        e();
        Items.Remove(e);
    }
}

public class GameEvent : UnityEvent
{
    public void AddToFront()
    {
        EventManager.Queue.AddToFront(() => Invoke());
    }

    public void AddToBack()
    {
        EventManager.Queue.AddToBack(() => Invoke());
    }
}

public class GameEvent<T0> : UnityEvent<T0>
{
    public void AddToFront(T0 arg0)
    {
        EventManager.Queue.AddToFront(() => Invoke(arg0));
    }

    public void AddToBack(T0 arg0)
    {
        EventManager.Queue.AddToBack(() => Invoke(arg0));
    }
}

public class GameEvent<T0, T1> : UnityEvent<T0, T1>
{
    public void AddToFront(T0 arg0, T1 arg1)
    {
        EventManager.Queue.AddToFront(() => Invoke(arg0, arg1));
    }

    public void AddToBack(T0 arg0, T1 arg1)
    {
        EventManager.Queue.AddToBack(() => Invoke(arg0, arg1));
    }
}

public class GameEvent<T0, T1, T2> : UnityEvent<T0, T1, T2>
{
    public void AddToFront(T0 arg0, T1 arg1, T2 arg2)
    {
        EventManager.Queue.AddToFront(() => Invoke(arg0, arg1, arg2));
    }

    public void AddToBack(T0 arg0, T1 arg1, T2 arg2)
    {
        EventManager.Queue.AddToBack(() => Invoke(arg0, arg1, arg2));
    }
}

public class GameEvent<T0, T1, T2, T3> : UnityEvent<T0, T1, T2, T3>
{
    public void AddToFront(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
    {
        EventManager.Queue.AddToFront(() => Invoke(arg0, arg1, arg2, arg3));
    }

    public void AddToBack(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
    {
        EventManager.Queue.AddToBack(() => Invoke(arg0, arg1, arg2, arg3));
    }
}
