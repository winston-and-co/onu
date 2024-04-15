using DG.Tweening.Plugins;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Unity.VisualScripting;
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
			//StartTracker();
			StartTracker2();
		}
	}

	public void StartTracker2()
	{
		MethodInfo printMethod = GetType().GetMethod(nameof(Print), BindingFlags.Static | BindingFlags.NonPublic);
		BattleEventBus bus = BattleEventBus.getInstance();
		FieldInfo[] events = bus.GetType().GetFields();

		foreach (var ev in events)
		{
			Type eventType = ev.FieldType.BaseType;
			Type[] eventArgs = eventType.GetGenericArguments();
			if (eventArgs.Length == 1)
			{
				Type actionType = typeof(UnityAction<>).MakeGenericType(eventArgs);
				MethodInfo addListener = typeof(UnityEvent<>)
					.MakeGenericType(eventArgs)
					.GetMethod(nameof(UnityEvent.AddListener));
				Delegate printer =
					Expression.Lambda(actionType,
						Expression.Call(printMethod,
							Expression.Constant(ev.Name),
							Expression.Constant(new string[] { })),
						false,
						new ParameterExpression[] {
							Expression.Parameter(eventArgs[0]),
						}).Compile();
				addListener.Invoke(ev.GetValue(bus), new object[] { printer });
				print($"Added listener for event: {ev.Name}");
			}
			else if (eventArgs.Length == 2)
			{
				Type actionType = typeof(UnityAction<,>).MakeGenericType(eventArgs);
				MethodInfo addListener = typeof(UnityEvent<,>)
					.MakeGenericType(eventArgs)
					.GetMethod(nameof(UnityEvent.AddListener));
				Delegate printer =
					Expression.Lambda(actionType,
						Expression.Call(printMethod,
							Expression.Constant(ev.Name),
							Expression.Constant(new string[] { })),
						false,
						new ParameterExpression[] {
							Expression.Parameter(eventArgs[0]),
							Expression.Parameter(eventArgs[1]),
						}).Compile();
				addListener.Invoke(ev.GetValue(bus), new object[] { printer });
				print($"Added listener for event: {ev.Name}");
			}
			else if (eventArgs.Length == 3)
			{
				Type actionType = typeof(UnityAction<,,>).MakeGenericType(eventArgs);
				MethodInfo addListener = typeof(UnityEvent<,,>)
					.MakeGenericType(eventArgs)
					.GetMethod(nameof(UnityEvent.AddListener));
				Delegate printer =
					Expression.Lambda(actionType,
						Expression.Call(printMethod,
							Expression.Constant(ev.Name),
							Expression.Constant(new string[] { })),
						false,
						new ParameterExpression[] {
							Expression.Parameter(eventArgs[0]),
							Expression.Parameter(eventArgs[1]),
							Expression.Parameter(eventArgs[2]),
						}).Compile();
				addListener.Invoke(ev.GetValue(bus), new object[] { printer });
				print($"Added listener for event: {ev.Name}");
			}
		}
		print("\n");
	}
	public void StartTracker()
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