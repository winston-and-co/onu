using System.Collections;
using UnityEngine;

public abstract class AbstractPattern : MonoBehaviour
{
    protected AbstractEntity entity;

    void Awake()
    {
        entity = GetComponent<EnemyEntity>();
        var eq = EventQueue.GetInstance();
        eq.startTurnEvent.AddListener(OnTurnStart);
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
        EventQueue.GetInstance().tryEndTurnEvent.AddToBack(entity);
    }

    protected abstract IEnumerator DoTurn();
}