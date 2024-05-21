using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bathroom : MonoBehaviour
{
    [SerializeField] BathroomOptions options;
    [SerializeField] BathroomUseButton useButton;
    [SerializeField] Button leaveButton;
    [SerializeField] Image fadeToBlack;
    [SerializeField] TMP_Text healText;

    bool usingBathroom;

    void Awake()
    {
        options.gameObject.SetActive(false);
        options.OptionPickedEvent.AddListener(OnOptionPicked);
        leaveButton.gameObject.SetActive(false);
        leaveButton.onClick.AddListener(OnLeaveBathroom);
        fadeToBlack.color = new Vector4(0, 0, 0, 0);
        fadeToBlack.gameObject.SetActive(false);
        healText.alpha = 0f;
        useButton.MouseDownEvent.AddListener(OnUseBathroom);
        useButton.SetGlowing(true);
        useButton.SetOpen(true);
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
        useButton.SetGlowing(false);
        useButton.SetOpen(false);
        usingBathroom = true;
        fadeToBlack.gameObject.SetActive(true);
        StartCoroutine(UseBRHelper());
    }

    void OnLeaveBathroom()
    {
        OnuSceneManager.GetInstance().ChangeScene(Scene.Map);
    }

    void OnOptionPicked()
    {
        StartCoroutine(EndUseBathroom());
    }

    IEnumerator UseBRHelper()
    {
        Tween t = fadeToBlack.DOColor(new Vector4(0, 0, 0, 0.998f), 1);
        yield return t.WaitForCompletion();
        yield return new WaitForSeconds(2);
        var p = PlayerData.GetInstance().Player;
        p.Heal(p.maxHP / 5);
        t = healText.DOFade(1f, 0.4f);
        yield return t.WaitForCompletion();
        if (Random.Range(0, 1f) <= 0.1f)
        {
            options.gameObject.SetActive(true);
        }
        else
        {
            StartCoroutine(EndUseBathroom());
        }
    }

    IEnumerator EndUseBathroom()
    {
        yield return new WaitForSeconds(2);
        healText.DOFade(0, 0.4f);
        options.gameObject.SetActive(false);
        Tween t = fadeToBlack.DOColor(new Vector4(0, 0, 0, 0), 1);
        yield return t.WaitForCompletion();
        fadeToBlack.gameObject.SetActive(false);
        leaveButton.gameObject.SetActive(true);
    }
}