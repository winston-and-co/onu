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
        Hide();
        AbstractActionCard actionCard = go.GetComponent<AbstractActionCard>();
        actionCard.TryUse();
        foreach (var s in spawned)
            Destroy(s);
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
        UIPopupBlocker.StopBlocking();
    }
}