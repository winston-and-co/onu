using System;

public interface IUsable
{
    public void TryUse();
    public bool IsUsable();
    public void Use(Action onResolved);
}