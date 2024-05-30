using UnityEngine;
using UnityEngine.Events;

public class BathroomUseButton : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite openSprite;
    [SerializeField] Sprite closedSprite;
    [SerializeField] GameObject glow;

    public UnityEvent MouseDownEvent = new();

    void Awake()
    {
        SetOpen(true);
        SetGlowing(true);
    }

    void OnMouseDown()
    {
        MouseDownEvent.Invoke();
        SetOpen(false);
        SetGlowing(false);
        Destroy(GetComponent<BoxCollider2D>());
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