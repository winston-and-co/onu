using System;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject firstBattleTutorial;
    [SerializeField] GameObject manaTutorial;
    [SerializeField] GameObject actionCardTutorial;
    [SerializeField] GameObject ruleCardTutorial;

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
        if (SettingsHelper.Instance.Deserialize().firstBattleTutorialSeen) return;
        StartTutorialSequence(firstBattleTutorial, () =>
        {
            var s = SettingsHelper.Instance.Deserialize();
            s.firstBattleTutorialSeen = true;
            SettingsHelper.Instance.Serialize(s);
        });
    }

    public void ManaTutorial()
    {
        if (SettingsHelper.Instance.Deserialize().manaTutorialSeen) return;
        StartTutorialSequence(manaTutorial, () =>
        {
            var s = SettingsHelper.Instance.Deserialize();
            s.manaTutorialSeen = true;
            SettingsHelper.Instance.Serialize(s);
        });
    }

    public void ActionCardsTutorial()
    {
        if (SettingsHelper.Instance.Deserialize().actionCardTutorialSeen) return;
        StartTutorialSequence(actionCardTutorial, () =>
        {
            var s = SettingsHelper.Instance.Deserialize();
            s.actionCardTutorialSeen = true;
            SettingsHelper.Instance.Serialize(s);
        });
    }

    public void RuleCardsTutorial()
    {
        if (SettingsHelper.Instance.Deserialize().ruleCardTutorialSeen) return;
        StartTutorialSequence(ruleCardTutorial, () =>
        {
            var s = SettingsHelper.Instance.Deserialize();
            s.ruleCardTutorialSeen = true;
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
        Blockers.GameBlocker.StartBlocking();
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
        Blockers.GameBlocker.StopBlocking();
    }
}
