using TMPro;
using UnityEngine;

public class HealthBarAdapter : MonoBehaviour
{
    [SerializeField] bool isPlayerAdapter;
    public Entity entity;
    public TMP_Text hpText;
    public Microlight.MicroBar.MicroBar bar;

    void Awake()
    {
        BattleEventBus.getInstance().startBattleEvent.AddListener(OnStartBattle);
        BattleEventBus.getInstance().entityHealthChangedEvent.AddListener(OnEntityHealthChanged);
    }

    void OnStartBattle(GameMaster gm)
    {
        entity = isPlayerAdapter ? gm.player : gm.enemy;
        bar.Initialize(entity.maxHP);
        bar.UpdateHealthBar(entity.hp);
        hpText.SetText($"{entity.hp} / {entity.maxHP}");
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
