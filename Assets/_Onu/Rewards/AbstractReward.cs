using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class AbstractReward : MonoBehaviour, IPointerClickHandler
{
    public TooltipOwner Tooltips;

    public static AbstractReward New<T>(string label, string spriteName) => New(typeof(T), label, spriteName);
    public static AbstractReward New(System.Type rewardType, string label, string spriteName)
    {
        GameObject go = PrefabLoader.GetInstance().InstantiatePrefab(PrefabType.RewardItem);
        AbstractReward reward = go.AddComponent(rewardType) as AbstractReward;
        Image image = go.GetComponentsInChildren<Image>()[1];
        image.sprite = SpriteLoader.LoadSprite(spriteName);
        TMP_Text tmp = go.GetComponentInChildren<TMP_Text>();
        tmp.text = label;
        reward.Tooltips = go.AddComponent<TooltipOwner>();
        return reward;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Collect();
        Destroy(gameObject);
    }

    public abstract void Collect();
}