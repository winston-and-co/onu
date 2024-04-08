
using System;
using System.Linq;
using UnityEngine;

public class EventTracker : MonoBehaviour
{
    [SerializeField]
    private bool Track;

    public void Start()
    {
        if (Track)
        {
            StartTracker();
        }
    }

    public static void StartTracker()
    {
        // gotta be a better way to do this with reflection but im too lazy to figure that out
        var bus = BattleEventBus.getInstance();
        bus.cardTryPlayedEvent.AddListener((_, _) => Print("tryPlayEvent"));
        bus.cardPlayedEvent.AddListener((_, _) => Print("cardPlayedEvent"));
        bus.cardIllegalEvent.AddListener((_, _) => Print("cardIllegalEvent"));
        bus.cardDrawEvent.AddListener((_, _) => Print("cardDrawEvent"));
        bus.cardNoDrawEvent.AddListener((_, _) => Print("cardNoDrawEvent"));
        bus.cardTryDrawEvent.AddListener((_, _) => Print("cardTryDrawEvent"));
        bus.entityDamageEvent.AddListener((_, _) => Print("entityDamageEvent"));
        bus.entityHealEvent.AddListener((_, _) => Print("entityHealEvent"));
        bus.entityHealthChangedEvent.AddListener((_, _) => Print("entityHealthChanged"));
        bus.entityManaSpentEvent.AddListener((_, _) => Print("entityManaSpentEvent"));
        bus.entityManaChangedEvent.AddListener((_, _) => Print("entityManaChangedEvent"));
        bus.entityRefreshEvent.AddListener((_) => Print("entityRefreshEvent"));

        bus.actionCardTryUseEvent.AddListener((_, _) => Print("actionCardTryUseEvent"));
        bus.actionCardUsedEvent.AddListener((_, _) => Print("actionCardUsedEvent"));
        bus.actionCardObtainedEvent.AddListener((_, _) => Print("actionCardObtainedEvent"));
        bus.actionCardDiscardedEvent.AddListener((_, _) => Print("actionCardDiscardedEvent"));

        bus.startBattleEvent.AddListener((_) => Print("startBattleEvent"));
        bus.startTurnEvent.AddListener((_) => Print("startTurnEvent"));
        bus.endTurnEvent.AddListener((_) => Print("endTurnEvent"));
        bus.tryEndTurnEvent.AddListener((_) => Print("tryEndTurnEvent"));
        bus.endBattleEvent.AddListener((_) => Print("endBattleEvent"));
    }

    static void Print(string eventName, params string[] args)
    {
        var output = $"{eventName}";
        if (args.Length > 0)
        {
            output += $"\t{string.Join(" ", args)}";
        }
        output += $"\t{DateTime.Now}";
        Debug.Log(output);
    }
}