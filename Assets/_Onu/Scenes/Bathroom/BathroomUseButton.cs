using UnityEngine;
using UnityEngine.Events;

public class BathroomUseButton : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite openSprite;
    [SerializeField] Sprite closedSprite;
    [SerializeField] GameObject glow;

    public UnityEvent MouseDownEvent = new();

    void OnMouseDown()
    {
        MouseDownEvent.Invoke();
    }

    public void SetGlowing(bool isGlowing)
    {
        glow.SetActive(isGlowing);
    }

    public void SetOpen(bool isOpen)
    {
        spriteRenderer.sprite = isOpen ? openSprite : closedSprite;
    }
}