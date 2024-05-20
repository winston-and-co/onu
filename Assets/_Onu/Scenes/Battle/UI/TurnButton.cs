using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnButton : MonoBehaviour
{
    [SerializeField] GameObject container;

    void Awake()
    {
        BattleEventBus.GetInstance().startTurnEvent.AddListener(OnTurnStart);
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnTurnStart(AbstractEntity e)
    {
        if (e != PlayerData.GetInstance().Player) return;
        GetComponent<Button>().interactable = true;
    }

    void OnClick()
    {
        if (GameMaster.GetInstance().current_turn_entity != PlayerData.GetInstance().Player) return;
        GetComponent<Button>().interactable = false;
        BattleEventBus.GetInstance().tryEndTurnEvent.Invoke(PlayerData.GetInstance().Player);
    }
}
