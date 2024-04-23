using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryLabel : MonoBehaviour
{
    TMP_Text victoryLabelText;
    public GameObject victoryScreen;
    public GameObject defeatScreen;


    void Awake()
    {
        victoryLabelText = GetComponent<TMP_Text>();
        victoryLabelText.SetText("");
        BattleEventBus.getInstance().endBattleEvent.AddListener(OnBattleEnd);
    }

    public void OnBattleEnd(GameMaster gm)
    {
        if (gm.PlayerWon())
        {
            victoryScreen.SetActive(true);
        }
        else
        {
            defeatScreen.SetActive(true);
        }


        victoryLabelText.SetText(gm.victor.e_name);
    }
}
