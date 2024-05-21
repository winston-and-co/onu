using System.Collections.Generic;
using Blockers;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] Button nextButton;
    [SerializeField] ScrollRect scrollRect;
    readonly List<AbstractReward> rewards = new();

    void Awake()
    {
        EventQueue.GetInstance().endBattleEvent.AddListener(OnEndBattle);
        Hide();
        nextButton.onClick.AddListener(OnClickNext);
    }

    void OnClickNext()
    {
        Hide();
        OnuSceneManager.GetInstance().ChangeScene(Scene.Map);
    }

    void OnEndBattle(GameMaster gm)
    {
        if (gm.Victor == gm.Player)
        {
            Show();
        }
    }

    void Show()
    {
        gameObject.SetActive(true);
        UIPopupBlocker.StartBlocking();
        GenerateRewards();
    }

    void Hide()
    {
        gameObject.SetActive(false);
        UIPopupBlocker.StopBlocking();
    }

    void GenerateRewards()
    {
        var maxManaReward = ManaReward.New();
        AddToRewardsList(maxManaReward);
        int numActionCards = Random.Range(0, 100) switch
        {
            >= 0 and < 70 => 1,
            >= 70 and < 90 => 2,
            >= 90 and < 98 => 3,
            _ => 4,
        };
        for (int i = 0; i < numActionCards; i++)
        {
            var actionCardReward = ActionCardReward.New();
            AddToRewardsList(actionCardReward);
        }
        int numRuleCards = Random.Range(0, 100) switch
        {
            >= 0 and < 70 => 1,
            >= 70 and < 90 => 2,
            >= 90 and < 98 => 3,
            _ => 4,
        };
        for (int i = 0; i < numRuleCards; i++)
        {
            var ruleCardReward = RuleCardReward.New();
            AddToRewardsList(ruleCardReward);
        }
    }

    float nextPosition = 0;
    void AddToRewardsList(AbstractReward reward)
    {
        rewards.Add(reward);
        var rt = reward.GetComponent<RectTransform>();
        rt.SetParent(scrollRect.content);
        rt.offsetMin = new Vector2(10f, rt.offsetMin.y);
        rt.offsetMax = new Vector2(-10f, rt.offsetMax.y);
        rt.anchoredPosition = new Vector2(0, nextPosition);
        nextPosition -= 120f;
    }
}