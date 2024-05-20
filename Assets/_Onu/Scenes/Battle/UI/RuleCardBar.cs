using UnityEngine;

public class RuleCardBar : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    public AbstractEntity entity;
    [SerializeField] GameObject root;
    [SerializeField] bool towardRight;

    void Awake()
    {
        BattleEventBus.GetInstance().startBattleEvent.AddListener(DisplayRuleCards);
        BattleEventBus.GetInstance().endBattleEvent.AddListener(CleanUp);
    }

    void DisplayRuleCards(GameMaster gm)
    {
        entity = isPlayer ? gm.player : gm.enemy;
        var rcs = entity.gameRulesController.Rules;
        int i = 0;
        foreach (var rc in rcs)
        {
            rc.transform.SetParent(root.transform);
            var rt = rc.GetComponent<RectTransform>();
            rt.pivot = new Vector2(towardRight ? 0 : 1, 1);
            rt.position = root.transform.position + new Vector3((towardRight ? 1 : -1) * (rt.rect.width / 2 + 10f) * i, 0, 0);
            i++;
        }
    }

    void CleanUp(GameMaster gm)
    {
        entity.gameRulesController.ReturnToContainer();
    }
}