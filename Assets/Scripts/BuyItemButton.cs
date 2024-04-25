using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyItemButton : MonoBehaviour
{
    public CurrencyLabel currency;
    public int cost;
    public TextMeshProUGUI costText;


    public void Start()
    {
        currency = FindObjectOfType<CurrencyLabel>();
        costText.text = cost.ToString();
    }

    public void TryBuy()
    {
        if (currency.CanAfford(cost))
        {
            currency.UpdateCurrency(-cost);
            gameObject.SetActive(false);
        }
    }
}
