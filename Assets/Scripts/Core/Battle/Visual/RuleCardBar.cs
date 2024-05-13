using UnityEngine;

public class RuleCardBar : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    public AbstractEntity entity;
    [SerializeField] GameObject root;
    [SerializeField] bool towardRight;

    void Awake()
    {
        BattleEventBus.getInstance().startBattleEvent.AddListener(DisplayRuleCards);
    }

    void DisplayRuleCards(GameMaster gm)
    {
        entity = isPlayer ? gm.player : gm.enemy;
        var rcs = entity.gameRules.Rules;
        int i = 0;
        foreach (var rc in rcs)
        {
            var prefab = Resources.Load<GameObject>($"Prefabs/RuleCards/{rc.Name}");
            if (prefab == null) continue;
            var go = Instantiate(prefab);
            go.transform.SetParent(root.transform);
            var rt = go.GetComponent<RectTransform>();
            rt.pivot = new Vector2(towardRight ? 0 : 1, 1);
            rt.position = root.transform.position + new Vector3((towardRight ? 1 : -1) * (rt.rect.width / 2 + 10f) * i, 0, 0);
            i++;
        }
    }
}