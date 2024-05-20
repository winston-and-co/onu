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
        EventQueue.GetInstance().endBattleEvent.AddListener(OnEndBattle);
        gameObject.SetActive(false);

        nextButton.onClick.AddListener(() => OnuSceneManager.GetInstance().ChangeScene(Scene.Map));
        // Show();
    }

    void OnEndBattle(GameMaster gm)
    {
        if (gm.victor == gm.player)
        {
            Show();
        }
    }

    void Show()
    {
        gameObject.SetActive(true);
        GenerateRewards();
    }

    void GenerateRewards()
    {
        int numActionCards = Random.Range(0, 100) switch
        {
            >= 0 and < 70 => 10,
            >= 70 and < 90 => 2,
            >= 90 and < 98 => 3,
            _ => 4,
        };
        for (int i = 0; i < numActionCards; i++)
        {
            var actionCardReward = ActionCardReward.New();
            AddToRewardsList(actionCardReward);
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