using UnityEngine;
using UnityEngine.EventSystems;

public class MaxManaService : AbstractShadyManService
{
    public override bool ConditionMet()
    {
        return true;
    }

    [SerializeField] int amount;
    public override void OnPointerClick(PointerEventData eventData)
    {
        PlayerData.GetInstance().Player.ChangeMaxMana(amount);
        base.Done();
    }
}