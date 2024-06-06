using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Bathroom : MonoBehaviour
{
    [SerializeField] GameObject eventContainer;
    [SerializeField] BathroomUseButton useButton;
    [SerializeField] Button leaveButton;
    [SerializeField] Image fadeToBlack;
    [SerializeField] TMP_Text healText;
    [SerializeField] float eventChance = 0.1f;
    [SerializeField] List<GameObject> eventPrefabs;

    bool usingBathroom;

    void Awake()
    {
        eventContainer.SetActive(false);
        leaveButton.gameObject.SetActive(false);
        leaveButton.onClick.AddListener(OnLeaveBathroom);
        fadeToBlack.color = new Vector4(0, 0, 0, 0);
        fadeToBlack.gameObject.SetActive(false);
        healText.alpha = 0f;
        useButton.MouseDownEvent.AddListener(OnUseBathroom);
    }

    IEnumerator Start()
    {
        usingBathroom = false;
        yield return new WaitForSeconds(5);
        if (!usingBathroom)
        {
            leaveButton.gameObject.SetActive(true);
        }
    }

    void OnUseBathroom()
    {
        leaveButton.gameObject.SetActive(false);
        usingBathroom = true;
        fadeToBlack.gameObject.SetActive(true);
        StartCoroutine(UseBathroomHelper());
    }

    void OnLeaveBathroom()
    {
        OnuSceneManager.GetInstance().ChangeScene(SceneType.Map);
    }

    void TriggerRandomEvent()
    {
        eventContainer.GetComponent<Image>().color = new(0, 0, 0, 0.998f);
        eventContainer.SetActive(true);
        fadeToBlack.gameObject.SetActive(false);
        var prefab = eventPrefabs[Random.Range(0, eventPrefabs.Count)];
        var go = Instantiate(prefab, eventContainer.transform);
        go.SetActive(true);
    }

    public void EventDone()
    {
        eventContainer.SetActive(false);
        StartCoroutine(UseBathroomEndAnimation());
    }

    IEnumerator UseBathroomHelper()
    {
        Tween t = fadeToBlack.DOFade(0.998f, 1);
        yield return t.WaitForCompletion();
        yield return new WaitForSeconds(2);
        var p = PlayerData.GetInstance().Player;
        p.Heal(p.maxHP / 5);
        t = healText.DOFade(1f, 0.4f);
        yield return t.WaitForCompletion();
        yield return new WaitForSeconds(1);
        if (Random.Range(0, 1f) <= eventChance)
        {
            t = healText.DOFade(0f, 0.4f);
            yield return t.WaitForCompletion();
            yield return new WaitForSeconds(1);
            TriggerRandomEvent();
        }
        else
        {
            StartCoroutine(UseBathroomEndAnimation());
        }
    }

    IEnumerator UseBathroomEndAnimation()
    {
        healText.DOFade(0, 0.4f);
        Tween t = fadeToBlack.DOColor(new Vector4(0, 0, 0, 0), 1);
        yield return t.WaitForCompletion();
        fadeToBlack.gameObject.SetActive(false);
        leaveButton.gameObject.SetActive(true);
    }
}