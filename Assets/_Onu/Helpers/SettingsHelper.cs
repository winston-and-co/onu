using System;
using System.IO;
using UnityEngine;

[Serializable]
public class Settings
{
    public bool tutorial1Seen;
    public bool tutorial2Seen;
    public bool tutorial3Seen;
}

public class SettingsHelper
{
    static SettingsHelper _instance;
    public static SettingsHelper Instance => _instance ??= new();

    public string Filepath;

    SettingsHelper()
    {
        string filename = "settings.json";
        string path = System.IO.Path.Join(Application.persistentDataPath, filename);
        if (File.Exists(path))
        {
            Debug.Log("Settings file found.");
        }
        else
        {
            Debug.Log("Settings file not found. Creating file.");
            File.Create(path);
            Serialize(new Settings());
        }
        this.Filepath = path;
        Debug.Log("Filepath: " + path);
    }

    public void Serialize(Settings settings)
    {
        string json = JsonUtility.ToJson(settings, true);
        File.WriteAllText(Filepath, json);
    }

    public Settings Deserialize()
    {
        string jsonText = File.ReadAllText(Filepath);
        Settings obj = JsonUtility.FromJson<Settings>(jsonText);
        return obj;
    }
}