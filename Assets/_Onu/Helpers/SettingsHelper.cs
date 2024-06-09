using System;
using System.IO;
using System.Text;
using UnityEngine;

[Serializable]
public class Settings
{
    public bool tutorial1Seen = false;
    public bool tutorial2Seen = false;
    public bool tutorial3Seen = false;
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
        this.Filepath = path;
        if (File.Exists(path))
        {
            Debug.Log("Settings file found.");
        }
        else
        {
            Debug.Log("Settings file not found. Creating file.");
            using FileStream fs = File.Create(path);
            string json = JsonUtility.ToJson(new Settings(), true);
            fs.Write(Encoding.UTF8.GetBytes(json));
        }
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