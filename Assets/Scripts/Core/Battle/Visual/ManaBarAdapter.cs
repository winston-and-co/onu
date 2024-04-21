using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManaBarAdapter : MonoBehaviour
{
    public Entity entity;
    public TMP_Text manaText;
    public Microlight.MicroBar.MicroBar bar;

    void Awake()
    {
        BattleEventBus.getInstance().startBattleEvent.AddListener((_) =>
        {
            manaText.SetText($"{entity.mana} / {entity.maxMana}");
        });
        BattleEventBus.getInstance().entityManaChangedEvent.AddListener(OnEntityManaChanged);

        bar.Initialize(entity.maxMana);
    }

    void OnEntityManaChanged(Entity e, int _)
    {
        if (e == entity)
        {
            bar.UpdateHealthBar(e.mana);
            manaText.SetText($"{entity.mana} / {entity.maxMana}");
        }
    }
}
