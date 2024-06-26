using TMPro;
using UnityEngine;

public class HealthBarAdapter : MonoBehaviour
{
    [SerializeField] bool isPlayerAdapter;
    public AbstractEntity entity;
    public TMP_Text hpText;
    public Microlight.MicroBar.MicroBar bar;

    void Awake()
    {
        EventManager.startedBattleEvent.AddListener(OnStartBattle);
        EventManager.entityHealthChangedEvent.AddListener(OnEntityHealthChanged);
    }

    void OnStartBattle()
    {
        var gm = GameMaster.GetInstance();
        entity = isPlayerAdapter ? gm.Player : gm.Enemy;
        bar.Initialize(entity.maxHP);
        bar.UpdateHealthBar(entity.hp);
        hpText.SetText($"{entity.hp} / {entity.maxHP}");
    }

    void OnEntityHealthChanged(AbstractEntity e, int _)
    {
        if (e == entity)
        {
            bar.UpdateHealthBar(e.hp);
            hpText.SetText($"{entity.hp} / {entity.maxHP}");
        }
    }
}
