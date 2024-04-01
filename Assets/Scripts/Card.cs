using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Card : MonoBehaviour
{

    public int value;
    public Color color;
    protected int lastOrder;
    public UnityEvent m_PlayEvent;
    private CardSprite spriteController;
    public Entity entity;

    public void OnEnable()
    {
        spriteController = GetComponentInChildren<CardSprite>();
    }

    string GetColor()
    {
        throw new NotImplementedException();
    }

    int GetDamage()
    {
        throw new NotImplementedException();
    }

    public void OnMouseDown()
    {
        BattleEventBus.getInstance().tryPlayEvent.Invoke(entity,this);
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
