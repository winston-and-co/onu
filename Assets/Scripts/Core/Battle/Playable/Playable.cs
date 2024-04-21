using UnityEngine;

public class Playable : MonoBehaviour
{
    public NullableInt Value;

    [SerializeField]
    private Color _color;
    public Color Color
    {
        get => _color;
        set
        {
            _color = value;
            if (TryGetComponent<SpriteRenderer>(out var sr))
            {
                sr.color = value;
                return;
            }
            sr = GetComponentInChildren<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = value;
                return;
            }
        }
    }

    // "owner"
    public Entity entity;

    public virtual bool IsPlayable()
    {
        return entity.gameRules.CardIsPlayable(GameMaster.GetInstance(), entity, this).Result;
    }
}