using System.Collections.Generic;

public class TextFormat
{
    static readonly Dictionary<string, string> keywordColors = new()
    {
        ["HP"] = "#FF5959",
        ["Mana"] = "#929BFF",
        ["Colors"] = "yellow",
        ["Color"] = "yellow",
        ["Values"] = "yellow",
        ["Value"] = "yellow",
        ["Action Cards"] = "yellow",
        ["Action Card"] = "yellow",
        ["Rule Cards"] = "yellow",
        ["Rule Card"] = "yellow",
    };
    public static string FormatKeywords(string input)
    {
        foreach ((string keyword, string colorName) in keywordColors)
        {
            if (input.Contains($"_{keyword}"))
            {
                input = input.Replace($"_{keyword}", keyword);
                continue;
            }
            input = input.Replace(keyword, $"<b><color={colorName}>{keyword}</color></b>");
        }
        return input;
    }
}