using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManaBarAdapter : MonoBehaviour
{
    [SerializeField] bool isPlayerAdapter;
    public Entity entity;
    public TMP_Text manaText;
    public Microlight.MicroBar.MicroBar bar;

    void Awake()
    {
        BattleEventBus.getInstance().startBattleEvent.AddListener(OnStartBattle);
        BattleEventBus.getInstance().entityHealthChangedEvent.AddListener(OnEntityManaChanged);
    }

    void OnStartBattle(GameMaster gm)
    {
        entity = isPlayerAdapter ? gm.player : gm.enemy;
        bar.Initialize(entity.maxMana);
        manaText.SetText($"{entity.mana} / {entity.maxMana}");
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
