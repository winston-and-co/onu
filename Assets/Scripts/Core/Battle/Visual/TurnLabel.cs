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
        BattleEventBus.getInstance().startTurnEvent.AddListener(OnTurnStart);
    }

    public void OnTurnStart(Entity e)
    {

        turnLabelText.SetText(e.e_name);
    }
}
