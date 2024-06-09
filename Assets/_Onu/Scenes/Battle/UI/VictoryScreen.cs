using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] Button nextButton;
    [SerializeField] ScrollRect scrollRect;
    readonly List<AbstractReward> rewards = new();

    void Awake()
    {
        EventManager.endedBattleEvent.AddListener(OnEndBattle);
        Hide();
        nextButton.onClick.AddListener(OnClickNext);
    }

    void OnClickNext()
    {
        Hide();
        OnuSceneManager.GetInstance().ChangeScene(SceneType.Map);
    }

    void OnEndBattle()
    {
        var gm = GameMaster.GetInstance();
        if (gm.Victor == gm.Player)
        {
            Show();
        }
    }

    void Show()
    {
        gameObject.SetActive(true);
        Blockers.GameBlocker.StartBlocking();
        GenerateRewards();
    }

    void Hide()
    {
        gameObject.SetActive(false);
        Blockers.GameBlocker.StopBlocking();
    }

    void GenerateRewards()
    {
        var rand = new System.Random();
        bool chanceManaReward = rand.Next(2) == 1;
        if (chanceManaReward)
        {
            var maxManaReward = ManaReward.New();
            AddToRewardsList(maxManaReward);
        }
        int numActionCards = rand.Next(100) switch
        {
            >= 0 and < 90 => 1,
            >= 90 and < 98 => 2,
            _ => 3,
        };
        for (int i = 0; i < numActionCards; i++)
        {
            var actionCardReward = ActionCardReward.New();
            AddToRewardsList(actionCardReward);
        }
        int numRuleCards = rand.Next(100) switch
        {
            >= 0 and < 70 => 0,
            >= 70 and < 99 => 1,
            _ => 2,
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