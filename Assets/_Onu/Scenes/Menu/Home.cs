using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
    [SerializeField] GameObject charSelectPanel;
    [SerializeField] Button charSelectPanelButton;
    CharacterSelect charSelect;
    [SerializeField] float transitionDuration = 0.2f;

    void Awake()
    {
        charSelectPanel.SetActive(true);
        charSelect = charSelectPanel.GetComponentInChildren<CharacterSelect>();
        charSelect.OnConfirmSelection.AddListener(StartGame);
        var rt = (RectTransform)charSelectPanel.transform;
        float h = rt.rect.height;
        rt.offsetMin = new(rt.offsetMin.x, h);
        rt.offsetMax = new(rt.offsetMax.x, h);

        charSelectPanelButton.onClick.AddListener(ShowCharSelectPanel);
    }

    void ShowCharSelectPanel()
    {
        charSelectPanel.transform.DOLocalMoveY(0, transitionDuration);
    }

    void StartGame()
    {
        PlayerData.GetInstance().Player.gameObject.SetActive(false);
        PlayerData.GetInstance().Player.deck.gameObject.SetActive(false);

        EventManager.startGameEvent.AddToFront();
        OnuSceneManager.GetInstance().ChangeScene(SceneType.Map);
    }
}