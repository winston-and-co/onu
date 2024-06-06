using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour
{
    static Map Instance;
    public static Map GetInstance() => Instance;

    [SerializeField] GameObject levelPrefab;

    public Level[] levels;
    public DrawPath path;
    public GameObject pathParent;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Generate();
        DrawPaths();
    }

    public void Generate()
    {
        // TODO: use level prefab
        levels[0].levelIdx = 0;
        levels[0].Generate();
        for (int i = 0; i < levels.Length - 1; i++)
        {
            Level currentLevel = levels[i];
            Level nextLevel = levels[i + 1];
            nextLevel.levelIdx = i + 1;
            nextLevel.Generate();

            CreateConnections(currentLevel, nextLevel);
        }
    }

    public void DrawPaths()
    {
        for (int i = 0; i < levels.Length - 1; i++)
        {
            Level currentLevel = levels[i];
            foreach (MapNode fromNode in currentLevel.nodes)
            {
                foreach (MapNode toNode in fromNode.ConnectedNodes)
                {
                    DrawPath newpath = Instantiate(path);
                    newpath.transform.parent = pathParent.transform;
                    newpath.Draw(fromNode, toNode);
                }
            }
        }
    }

    MapNode GetRandomNode(List<MapNode> nodes)
    {
        return nodes[UnityEngine.Random.Range(0, nodes.Count)];
    }

    /// <summary>
    /// Creates connections from one level to another level.
    /// </summary>
    /// <param name="minConns">The minimum outdegree of a node.</param>
    /// <param name="maxConns">The maximum outdegree of a node.</param>
    void CreateConnections(Level from, Level to, int minConns = 1, int maxConns = 3)
    {
        HashSet<MapNode> connected = new();

        foreach (var node in from.nodes)
        {
            int targetNumConnections = Math.Min(UnityEngine.Random.Range(minConns, maxConns), to.nodes.Count);
            int n = 0;
            while (n < targetNumConnections)
            {
                var target = GetRandomNode(to.nodes);
                var added = node.AddConnection(target);
                connected.Add(target);
                if (added) n++;
            }
        }

        // connect nodes that are unreachable
        foreach (var node in to.nodes)
        {
            if (connected.Contains(node)) continue;
            GetRandomNode(from.nodes).AddConnection(node);
        }
    }
}
