using System.Collections;
using UnityEngine;

public abstract class AbstractPattern : MonoBehaviour
{
    protected AbstractEntity entity;

    void Awake()
    {
        entity = GetComponent<EnemyEntity>();
        EventManager.startTurnEvent.AddListener(OnTurnStart);
    }

    void OnTurnStart(AbstractEntity e)
    {
        if (entity != e) return;
        StartCoroutine(DoTurnWrapper());
    }

    IEnumerator DoTurnWrapper()
    {
        yield return DoTurn();
        print("here");
        EventManager.tryEndTurnEvent.AddToBack(entity);
    }

    protected abstract IEnumerator DoTurn();
}