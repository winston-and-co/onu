using System.Collections.Generic;

public class TextFormat
{
    static readonly Dictionary<string, string> keywordColors = new()
    {
        ["Color"] = "yellow",
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
            input = input.Replace(keyword, $"<b><color=\"{colorName}\">{keyword}</color></b>");
        }
        return input;
    }
}