using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyLabel : MonoBehaviour
{
    public TextMeshProUGUI currencyText;
    public int currencyValue;



    void Start()
    {
        currencyText = GetComponentInChildren<TextMeshProUGUI>();
        currencyText.text = currencyValue.ToString();

    }

    public void UpdateCurrency(int amount)
    {
        currencyValue += amount;
        currencyText.text = currencyValue.ToString();
    }
    public bool CanAfford(int amount)
    {
        return currencyValue >= amount;
    }

    


}
