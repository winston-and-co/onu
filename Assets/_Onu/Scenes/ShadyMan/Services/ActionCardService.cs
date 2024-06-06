using UnityEngine.EventSystems;

public class ActionCardService : AbstractShadyManService
{
    public override bool ConditionMet()
    {
        return true;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        int id = ActionCardFactory.GetRandomId();
        PlayerData.GetInstance().AddActionCard(id);
        base.Done();
    }
}