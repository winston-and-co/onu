using ActionCards;
using TMPro;
using UnityEngine;

public class CardInRailing : MonoBehaviour
{
    AbstractActionCard actionCard;
    [SerializeField] TMP_Text bodyText;
    [SerializeField] TooltipOwner takeTooltip;

    void Awake()
    {
        actionCard = ActionCardFactory.MakeRandom();
        actionCard.gameObject.SetActive(false);
        bodyText.text = $"You find a card in the railing. Upon closer inspection, it is a(n) [{actionCard.Name}].";
        takeTooltip.Add(new()
        {
            Title = "Card in the Railing",
            Body = $"Obtain a(n) [{actionCard.Name}].",
        });
    }

    public void TakeIt()
    {
        PlayerData.GetInstance().AddActionCard(actionCard);
        GetComponent<BathroomEvent>().Done();
    }

    public void DontTakeIt()
    {
        Destroy(actionCard.gameObject);
        GetComponent<BathroomEvent>().Done();
    }
}
