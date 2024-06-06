using UnityEngine;
using UnityEngine.EventSystems;

public class MaxHPService : AbstractShadyManService
{
    public override bool ConditionMet()
    {
        return true;
    }

    [SerializeField] int amount;
    public override void OnPointerClick(PointerEventData eventData)
    {
        PlayerData.GetInstance().Player.ChangeMaxHP(amount);
        base.Done();
    }
}