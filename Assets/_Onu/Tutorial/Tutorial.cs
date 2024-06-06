using System;
using Blockers;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject tutorial1;
    [SerializeField] GameObject tutorial2;
    [SerializeField] GameObject tutorial3;

    private GameObject[] parts;
    private int index;
    Action endSequenceCallback;

    public static Tutorial Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void OnDestroy()
    {
        Instance = null;
    }

    public void FirstBattleTutorial()
    {
        if (SettingsHelper.Instance.Deserialize().tutorial1Seen) return;
        StartTutorialSequence(tutorial1, () =>
        {
            var s = SettingsHelper.Instance.Deserialize();
            s.tutorial1Seen = true;
            SettingsHelper.Instance.Serialize(s);
        });
    }

    public void ActionCardsTutorial()
    {
        if (SettingsHelper.Instance.Deserialize().tutorial2Seen) return;
        StartTutorialSequence(tutorial2, () =>
        {
            var s = SettingsHelper.Instance.Deserialize();
            s.tutorial2Seen = true;
            SettingsHelper.Instance.Serialize(s);
        });
    }

    public void RuleCardsTutorial()
    {
        if (SettingsHelper.Instance.Deserialize().tutorial3Seen) return;
        StartTutorialSequence(tutorial3, () =>
        {
            var s = SettingsHelper.Instance.Deserialize();
            s.tutorial3Seen = true;
            SettingsHelper.Instance.Serialize(s);
        });
    }

    void StartTutorialSequence(GameObject root, Action onEnd)
    {
        index = 0;
        foreach (var tmpText in root.GetComponentsInChildren<TMP_Text>(true))
        {
            tmpText.text = TextFormat.FormatKeywords(tmpText.text);
        }
        root.SetActive(true);
        parts = new GameObject[root.transform.childCount];
        for (int i = 0; i < root.transform.childCount; i++)
        {
            parts[i] = root.transform.GetChild(i).gameObject;
        }
        parts[0].SetActive(true);
        endSequenceCallback = onEnd;
        UIPopupBlocker.StartBlocking();
    }

    public void Advance()
    {
        parts[index].SetActive(false);
        index++;
        if (index < parts.Length)
        {
            parts[index].SetActive(true);
        }
        else
        {
            EndTutorialSequence();
        }
    }

    void EndTutorialSequence()
    {
        endSequenceCallback();
        UIPopupBlocker.StopBlocking();
    }
}
