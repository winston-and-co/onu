using System.Collections;
using UnityEngine;

public abstract class AbstractPattern : MonoBehaviour
{
    protected AbstractEntity entity;

    void Awake()
    {
        entity = GetComponent<EnemyEntity>();
        EventManager.startedTurnEvent.AddListener(OnTurnStart);
    }

    void OnTurnStart(AbstractEntity e)
    {
        if (entity != e) return;
        StartCoroutine(DoTurnWrapper());
    }

    IEnumerator DoTurnWrapper()
    {
        yield return DoTurn();
        EventManager.tryEndTurnEvent.AddToBack(entity);
    }

    protected abstract IEnumerator DoTurn();
}