using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnButton : MonoBehaviour
{
    [SerializeField] Entity player;
    public void TryEndTurn()
    {
        BattleEventBus.getInstance().tryEndTurnEvent.Invoke(player);
    }
}
