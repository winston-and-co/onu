using System;
using UnityEngine;

public class SpriteLoader
{
    /// <summary>
    /// Returns the sprite from given sprite name and asset version or null if not found.
    /// </summary>
    public static Sprite LoadSprite(string spriteName, AssetVersion version = AssetVersion.Alpha)
    {
        string ver = Enum.GetName(typeof(AssetVersion), version);
        return Load($"Sprites/{ver}/{spriteName}");
    }

    /// <summary>
    /// Usually you should use LoadSprite. Returns the sprite found using the Resources API or null if not found.
    /// </summary>
    public static Sprite Load(string path)
    {
        return Resources.Load<Sprite>(path);
    }
}

public enum AssetVersion
{
    Alpha
}
