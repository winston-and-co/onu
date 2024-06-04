using System;
using Blockers;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    GameObject tutorial1;

    private GameObject[] parts;
    private int index;
    Action endSequenceCallback;

    static Tutorial Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        EventManager.startedBattleEvent.AddListener(FirstBattleTutorial);
    }

    void FirstBattleTutorial()
    {
        if (SettingsHelper.Instance.Deserialize().tutorial1Seen)
        {
            EventManager.startedBattleEvent.RemoveListener(FirstBattleTutorial);
            return;
        }

        StartTutorialSequence(tutorial1, () =>
        {
            var s = SettingsHelper.Instance.Deserialize();
            s.tutorial1Seen = true;
            SettingsHelper.Instance.Serialize(s);
        });
    }

    void StartTutorialSequence(GameObject root, Action onEnd)
    {
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
