using TMPro;
using UnityEngine;

public class ManaBarAdapter : MonoBehaviour
{
    [SerializeField] bool isPlayerAdapter;
    public AbstractEntity entity;
    public TMP_Text manaText;
    public Microlight.MicroBar.MicroBar bar;

    void Awake()
    {
        BattleEventBus.GetInstance().startBattleEvent.AddListener(OnStartBattle);
        BattleEventBus.GetInstance().entityManaChangedEvent.AddListener(OnEntityManaChanged);
    }

    void OnStartBattle(GameMaster gm)
    {
        entity = isPlayerAdapter ? gm.player : gm.enemy;
        bar.Initialize(entity.maxMana);
        bar.UpdateHealthBar(entity.mana);
        manaText.SetText($"{entity.mana} / {entity.maxMana}");
    }

    void OnEntityManaChanged(AbstractEntity e, int _)
    {
        if (e == entity)
        {
            bar.UpdateHealthBar(e.mana);
            manaText.SetText($"{entity.mana} / {entity.maxMana}");
        }
    }
}
