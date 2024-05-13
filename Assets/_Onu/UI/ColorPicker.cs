
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    public UnityEvent<Color> OnColorPicked = new();

    Button redBtn;
    Button blueBtn;
    Button greenBtn;
    Button yellowBtn;

    void Start()
    {
        var redT = gameObject.transform.Find("OptionRed");
        redBtn = redT.GetComponent<Button>();
        var blueT = gameObject.transform.Find("OptionBlue");
        blueBtn = blueT.GetComponent<Button>();
        var greenT = gameObject.transform.Find("OptionGreen");
        greenBtn = greenT.GetComponent<Button>();
        var yellowT = gameObject.transform.Find("OptionYellow");
        yellowBtn = yellowT.GetComponent<Button>();

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
        Destroy(this);
    }
}