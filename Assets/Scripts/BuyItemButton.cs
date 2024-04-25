using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItemButton : MonoBehaviour
{
    public CurrencyLabel currency;
    public int cost;


    public void Start()
    {
        currency = FindObjectOfType<CurrencyLabel>();
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
