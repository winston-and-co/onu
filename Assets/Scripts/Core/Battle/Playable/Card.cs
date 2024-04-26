using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

/// <summary>
/// A subset of Playables that are regular uno cards.
/// </summary>
public class Card : Playable
{
    private CardSprite spriteController;

    void OnEnable()
    {
        spriteController = GetComponentInChildren<CardSprite>();
    }

    public void OnMouseDown()
    {
        BattleEventBus.getInstance().cardTryPlayedEvent.Invoke(entity, this);
    }

    public void OnMouseEnter()
    {
        spriteController.MouseEnter();
    }
    public void OnMouseExit()
    {
        spriteController.MouseLeave();
    }
}
