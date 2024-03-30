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
    private GameMaster gameMaster;
    private CardSprite spriteController;

    public void OnEnable()
    {
        gameMaster = GameObject.FindAnyObjectByType<GameMaster>();
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
        gameMaster.tryPlayEvent.Invoke(this);
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
