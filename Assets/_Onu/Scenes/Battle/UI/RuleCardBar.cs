using System.Collections.Generic;
using UnityEngine;

public class RuleCardBar : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    public AbstractEntity entity;
    [SerializeField] GameObject root;
    [SerializeField] bool towardRight;

    readonly List<GameObject> gameObjects = new();

    void Awake()
    {
        EventManager.startedBattleEvent.AddListener(DisplayRuleCards);
    }

    void DisplayRuleCards()
    {
        var gm = GameMaster.GetInstance();
        entity = isPlayer ? gm.Player : gm.Enemy;
        var rcs = entity.gameRulesController.Rules;
        int i = 0;
        foreach (var rc in rcs)
        {
            var obj = rc.ToGameObject();
            obj.transform.SetParent(root.transform);
            var rt = obj.GetComponent<RectTransform>();
            rt.pivot = new Vector2(towardRight ? 0 : 1, 1);
            rt.position = root.transform.position + new Vector3((towardRight ? 1 : -1) * (rt.rect.width / 2 + 10f) * i, 0, 0);
            i++;
            gameObjects.Add(obj);
        }
    }

    void OnDestroy()
    {
        for (int i = gameObjects.Count - 1; i >= 0; i--)
        {
            Destroy(gameObjects[i]);
            gameObjects.RemoveAt(i);
        }
    }
}