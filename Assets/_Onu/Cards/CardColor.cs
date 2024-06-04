using UnityEngine;

public class CardColor
{
    public static Color Red => new Color32(233, 0, 0, 255);
    public static Color Blue => new Color32(15, 59, 255, 255);
    public static Color Green => new Color32(0, 224, 4, 255);
    public static Color Yellow => new Color32(255, 216, 0, 255);
    public static Color Colorless => new Color32(255, 255, 255, 255);

    readonly static System.Random rand = new();
    public static Color RandomColor()
    {
        return rand.Next(4) switch
        {
            0 => Red,
            1 => Blue,
            2 => Green,
            3 => Yellow,
            _ => Red,
        };
    }

    public static Color RandomColorOrColorless()
    {
        return rand.Next(5) switch
        {
            0 => Red,
            1 => Blue,
            2 => Green,
            3 => Yellow,
            4 => Colorless,
            _ => Red,
        };
    }
}
