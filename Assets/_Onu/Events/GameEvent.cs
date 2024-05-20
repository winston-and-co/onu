using UnityEngine.Events;

public class GameEvent : UnityEvent
{
    public void AddToFront()
    {
        EventQueue.GetInstance().AddToFront(() => Invoke());
    }

    public void AddToBack()
    {
        EventQueue.GetInstance().AddToBack(() => Invoke());
    }
}

public class GameEvent<T0> : UnityEvent<T0>
{
    public void AddToFront(T0 arg0)
    {
        EventQueue.GetInstance().AddToFront(() => Invoke(arg0));
    }

    public void AddToBack(T0 arg0)
    {
        EventQueue.GetInstance().AddToBack(() => Invoke(arg0));
    }
}

public class GameEvent<T0, T1> : UnityEvent<T0, T1>
{
    public void AddToFront(T0 arg0, T1 arg1)
    {
        EventQueue.GetInstance().AddToFront(() => Invoke(arg0, arg1));
    }

    public void AddToBack(T0 arg0, T1 arg1)
    {
        EventQueue.GetInstance().AddToBack(() => Invoke(arg0, arg1));
    }
}

public class GameEvent<T0, T1, T2> : UnityEvent<T0, T1, T2>
{
    public void AddToFront(T0 arg0, T1 arg1, T2 arg2)
    {
        EventQueue.GetInstance().AddToFront(() => Invoke(arg0, arg1, arg2));
    }

    public void AddToBack(T0 arg0, T1 arg1, T2 arg2)
    {
        EventQueue.GetInstance().AddToBack(() => Invoke(arg0, arg1, arg2));
    }
}

public class GameEvent<T0, T1, T2, T3> : UnityEvent<T0, T1, T2, T3>
{
    public void AddToFront(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
    {
        EventQueue.GetInstance().AddToFront(() => Invoke(arg0, arg1, arg2, arg3));
    }

    public void AddToBack(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
    {
        EventQueue.GetInstance().AddToBack(() => Invoke(arg0, arg1, arg2, arg3));
    }
}
