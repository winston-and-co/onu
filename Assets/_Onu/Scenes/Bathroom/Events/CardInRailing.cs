using ActionCards;
using TMPro;
using UnityEngine;

public class CardInRailing : MonoBehaviour
{
    int actionCardId;
    [SerializeField] TMP_Text bodyText;
    [SerializeField] TooltipOwner takeTooltip;

    void Awake()
    {
        (var actionCard, var id) = ActionCardFactory.MakeRandom();
        actionCardId = id;
        actionCard.gameObject.SetActive(false);
        bodyText.text = $"You find a card in the railing. Upon closer inspection, it is a(n) [{actionCard.Name}].";
        takeTooltip.Add(new()
        {
            Title = "Card in the Railing",
            Body = $"Obtain a(n) [{actionCard.Name}].",
        });
        Destroy(actionCard.gameObject);
    }

    public void TakeIt()
    {
        PlayerData.GetInstance().AddActionCard(actionCardId);
        GetComponent<BathroomEvent>().Done();
    }

    public void DontTakeIt()
    {
        GetComponent<BathroomEvent>().Done();
    }
}
