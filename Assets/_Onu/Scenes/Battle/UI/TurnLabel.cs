using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnLabel : MonoBehaviour
{
    TMP_Text turnLabelText;

    void Awake()
    {
        turnLabelText = GetComponent<TMP_Text>();
        BattleEventBus.GetInstance().startTurnEvent.AddListener(OnTurnStart);
    }

    public void OnTurnStart(AbstractEntity e)
    {

        turnLabelText.SetText(e.e_name);
    }
}
