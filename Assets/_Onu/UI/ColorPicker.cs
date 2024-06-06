
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    public UnityEvent<Color> OnColorPicked = new();

    [SerializeField] Button redBtn;
    [SerializeField] Button blueBtn;
    [SerializeField] Button greenBtn;
    [SerializeField] Button yellowBtn;

    void Start()
    {
        redBtn.onClick.AddListener(() => OnClick(CardColor.Red));
        blueBtn.onClick.AddListener(() => OnClick(CardColor.Blue));
        greenBtn.onClick.AddListener(() => OnClick(CardColor.Green));
        yellowBtn.onClick.AddListener(() => OnClick(CardColor.Yellow));
    }

    void OnClick(Color color)
    {
        OnColorPicked.Invoke(color);
    }

    public void Show()
    {
        Blockers.UIPopupBlocker.StartBlocking();
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        Blockers.UIPopupBlocker.StopBlocking();
        gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        Blockers.UIPopupBlocker.StopBlocking();
    }
}