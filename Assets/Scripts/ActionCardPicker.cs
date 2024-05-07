using System.Collections.Generic;
using System.Linq;
using ActionCards;
using Blockers;
using UnityEngine;

public class ActionCardPicker : ScrollGridPicker
{
    List<GameObject> spawned;

    void Awake()
    {
        base.OnItemPicked.AddListener(OnActionCardPicked);
    }

    void OnActionCardPicked(GameObject go)
    {
        ActionCardBase actionCard = go.GetComponent<ActionCardBase>();
        Hide();
        if (actionCard is IUsable usable)
        {
            usable.TryUse();
        }
    }

    public new void Show()
    {
        UIPopupBlocker.StartBlocking();
        spawned = PlayerData.GetInstance().InstantiateActionCards()
            .Select(obj => obj.gameObject)
            .ToList();
        base.SetItems(spawned);
        base.Show();
    }

    public new void Hide()
    {
        base.Hide();
        foreach (var go in spawned)
            Destroy(go);
        UIPopupBlocker.StopBlocking();
    }
}