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

    [SerializeField]
    private bool ShowAddedListenerMessages;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        if (Track)
        {
            StartTracker();
        }
    }

    public void StartTracker()
    {
        print("Started event tracker");
        MethodInfo printMethod = GetType().GetMethod(nameof(Print), BindingFlags.Static | BindingFlags.NonPublic);
        EventQueue eq = EventQueue.GetInstance();
        FieldInfo[] events = eq.GetType().GetFields();

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
                addListener.Invoke(ev.GetValue(eq), new object[] { printer });
                if (ShowAddedListenerMessages)
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
                addListener.Invoke(ev.GetValue(eq), new object[] { printer });
                if (ShowAddedListenerMessages)
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
                addListener.Invoke(ev.GetValue(eq), new object[] { printer });
                if (ShowAddedListenerMessages)
                    print($"Added listener for event: {ev.Name}");
            }
        }
        if (ShowAddedListenerMessages)
            print("\n");
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