using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] GameObject hpObject;
    TMP_Text hpText;

    void Awake()
    {
        EventManager.entityHealthChangedEvent.AddListener(HPListener);
        EventManager.entityMaxHealthChangedEvent.AddListener(HPListener);

        hpText = hpObject.GetComponentInChildren<TMP_Text>();

        UpdateHealthBar();
    }

    void HPListener(AbstractEntity e, int _)
    {
        if (e != PlayerData.GetInstance().Player) return;
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        var playerE = PlayerData.GetInstance().Player;
        hpText.text = $"{playerE.hp}/{playerE.maxHP}";
    }
}