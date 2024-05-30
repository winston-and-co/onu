using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // Include this if you're using regular UI Text

public class TurnBanner : MonoBehaviour
{
    public TextMeshProUGUI turnBannerText; // Reference to the Text component
    public GameObject turnBanner; // Reference to the GameObject that holds the banner

    public Transform tbpos;

    void Awake()
    {
        // Optionally, start with the banner hidden
        //turnBanner.SetActive(false);

        tbpos = turnBanner.transform;

        EventManager.startTurnEvent.AddListener(ShowPlayerTurn);
    }

    // Call this method when it's a player's turn
    public void ShowPlayerTurn(AbstractEntity e)
    {
        turnBanner.SetActive(true);

        turnBannerText.text = $"{e.e_name}'s turn"; // Update the text

        var pos = turnBanner.transform.position;

        tbpos.position = new Vector3(710, tbpos.position.y, tbpos.position.z);

        tbpos.DOMoveX(pos.x, 1.0f);
        //bannerBackdrop.transform.DOMoveX(bdpos.position.x - 20.0f, 1.0f);

        StartTextEffects();

        // Optional: hide the banner after some time
        Invoke("HideBanner", 1.4f); // Wait 3 seconds before hiding
    }

    void StartTextEffects()
    {
        // Example of a scrolling effect from off-screen left to center
        //turnBannerText.rectTransform.anchoredPosition = new Vector2(-Screen.width, turnBannerText.rectTransform.anchoredPosition.y);
        //turnBannerText.rectTransform.DOAnchorPosX(0, 2.0f).SetEase(Ease.OutQuad); // Scroll to center in 2 seconds

        // Shaking effect
        //Sequence mySequence = DOTween.Sequence();
        //mySequence.Append(turnBannerText.rectTransform.DOShakePosition(2, new Vector3(10, 0, 0), 10, 90, false, false))
        //          .SetLoops(-1, LoopType.Restart); // Shake horizontally for 2 seconds and loop indefinitely
    }

    private void HideBanner()
    {
        turnBanner.SetActive(false);
    }
}
