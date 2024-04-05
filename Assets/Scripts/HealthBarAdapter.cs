using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthBarAdapter : MonoBehaviour
{
    public Entity entity;
    public TMP_Text hpText;
    public Microlight.MicroBar.MicroBar bar;

    void Awake()
    {
        BattleEventBus.getInstance().startBattleEvent.AddListener((_) =>
        {
            hpText.SetText($"{entity.hp} / {entity.maxHP}");
        });
        BattleEventBus.getInstance().entityHealthChangedEvent.AddListener(OnEntityHealthChanged);

        bar.Initialize(entity.maxHP);
    }

    void OnEntityHealthChanged(Entity e, int _)
    {
        if (e == entity)
        {
            bar.UpdateHealthBar(e.hp);
            hpText.SetText($"{entity.hp} / {entity.maxHP}");
        }
    }
}
