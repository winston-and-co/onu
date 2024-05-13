using System;
using UnityEngine;

public class SpriteLoader
{
    /// <summary>
    /// Returns the sprite from given asset version and sprite name.
    /// </summary>
    public static Sprite LoadSprite(string spriteName, AssetVersion version = AssetVersion.Alpha)
    {
        string ver = Enum.GetName(typeof(AssetVersion), version);
        return Load($"Sprites/{ver}/{spriteName}");
    }

    /// <summary>
    /// Returns the sprite found using the Resources API.
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
