using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class AbstractReward : MonoBehaviour, IPointerClickHandler
{
    public static AbstractReward New(System.Type rewardType, string label, string spriteName)
    {
        GameObject go = PrefabHelper.GetInstance().GetInstantiatedPrefab(PrefabType.RewardItem);
        AbstractReward reward = go.AddComponent(rewardType) as AbstractReward;
        print("new reward");
        Image image = go.GetComponentsInChildren<Image>()[1];
        image.sprite = SpriteLoader.LoadSprite(spriteName);
        TMP_Text tmp = go.GetComponentInChildren<TMP_Text>();
        tmp.text = label;
        return reward;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Collect();
    }

    public abstract void Collect();
}