using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ActionCards
{
    public class WildPrompt : MonoBehaviour
    {
        public Wild wild;
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

            redBtn.onClick.AddListener(() => OnClick(CardColors.Red));
            blueBtn.onClick.AddListener(() => OnClick(CardColors.Blue));
            greenBtn.onClick.AddListener(() => OnClick(CardColors.Green));
            yellowBtn.onClick.AddListener(() => OnClick(CardColors.Yellow));
        }

        void OnClick(Color color)
        {
            // must stop blocking before OnColorSelected
            Hide();
            wild.OnColorSelected(color);
        }

        public void Show()
        {
            UIPopupBlocker.StartBlocking();
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            UIPopupBlocker.StopBlocking();
            gameObject.SetActive(false);
            Destroy(this);
        }
    }
}