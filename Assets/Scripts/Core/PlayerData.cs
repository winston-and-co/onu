using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    static PlayerData Instance;
    public static PlayerData GetInstance() => Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Tuple of (level index, node index)
    /// </summary>
    public (int, int) CurrentNodeLocation = (-1, -1);
    public MapNode CurrentNode
    {
        get
        {
            if (CurrentNodeLocation.Item1 == -1 || CurrentNodeLocation.Item2 == -1)
            {
                return null;
            }
            return FindObjectOfType<Map>().levels[CurrentNodeLocation.Item1].nodes[CurrentNodeLocation.Item2];
        }
    }

    public Entity Player;
}
