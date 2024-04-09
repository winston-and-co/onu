using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

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

    // Written by Mr. GPT
    // but doesn't work
    // public static void StartTracker()
    // {
    //     print("start tracker");
    //     var bus = BattleEventBus.getInstance();
    //     var eventBusType = bus.GetType();
    //     var methods = typeof(BattleEventBus)
    //         .GetFields(BindingFlags.Public | BindingFlags.Instance)
    //         .Where(f => f.FieldType.IsGenericType &&
    //             f.FieldType.GetGenericTypeDefinition() == typeof(UnityEvent<>));
    //     print(typeof(BattleEventBus).GetFields(BindingFlags.Public | BindingFlags.Instance));

    //     foreach (var field in methods)
    //     {
    //         var eventType = field.FieldType.GetGenericArguments()[0];
    //         var addListenerMethod = eventBusType.GetMethod("AddListener").MakeGenericMethod(eventType);
    //         var eventName = field.Name;

    //         // Create a delegate for the Print method with appropriate parameters
    //         var printMethod = typeof(EventTracker).GetMethod("Print", BindingFlags.NonPublic | BindingFlags.Static);
    //         var printDelegateType = typeof(Action<,>).MakeGenericType(typeof(string), eventType);
    //         var printDelegate = Delegate.CreateDelegate(printDelegateType, null, printMethod);

    //         // Get the event field value
    //         var fieldValue = field.GetValue(bus);

    //         // Invoke the AddListener method on the event with the printDelegate
    //         addListenerMethod.Invoke(fieldValue, new object[] { printDelegate });

    //         Debug.Log($"Subscribed to {eventName} event.");
    //     }
    // }
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