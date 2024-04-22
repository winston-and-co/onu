using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Card : Playable
{
    protected int lastOrder;
    public UnityEvent m_PlayEvent;
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
