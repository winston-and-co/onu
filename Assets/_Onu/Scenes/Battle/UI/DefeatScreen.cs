using UnityEngine;
using UnityEngine.UI;

public class DefeatScreen : MonoBehaviour
{
    [SerializeField] Button nextButton;

    void Awake()
    {
        EventManager.endedBattleEvent.AddListener(OnEndBattle);
        Hide();
        nextButton.onClick.AddListener(OnClickNext);
    }

    void OnClickNext()
    {
        Hide();
        OnuSceneManager.GetInstance().ChangeScene(SceneType.MainMenu);
    }

    void Show()
    {
        gameObject.SetActive(true);
        Blockers.GameBlocker.StartBlocking();
    }

    void Hide()
    {
        gameObject.SetActive(false);
        Blockers.GameBlocker.StopBlocking();
    }

    void OnEndBattle()
    {
        var gm = GameMaster.GetInstance();
        if (gm.Victor != gm.Player)
        {
            Show();
        }
    }
}