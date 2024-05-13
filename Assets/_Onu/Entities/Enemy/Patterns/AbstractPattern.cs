using System.Collections;
using UnityEngine;

public abstract class AbstractPattern : MonoBehaviour
{
    protected AbstractEntity entity;

    void Awake()
    {
        entity = GetComponent<EnemyEntity>();
        var bus = BattleEventBus.getInstance();
        bus.startTurnEvent.AddListener(OnTurnStart);
    }

    void OnTurnStart(AbstractEntity e)
    {
        if (entity != e) return;
        StartCoroutine(DoTurnWrapper());
    }

    IEnumerator DoTurnWrapper()
    {
        yield return DoTurn();
        BattleEventBus.getInstance().tryEndTurnEvent.Invoke(entity);
    }

    protected abstract IEnumerator DoTurn();
}