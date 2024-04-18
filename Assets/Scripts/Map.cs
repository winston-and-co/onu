using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Level[] levels;  
    public DrawPath path;
    public GameObject pathParent;
    private static Map instance;

    void Awake()
    {
        if (instance != null) { 
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);   
    }
    void Start()
    {
        DrawPaths();
    }

    public void DrawPaths()
    {

        for (int i = 0; i < levels.Length-1; i++)
        {
            Level current = levels[i];
            Level next = levels[i+1];
            int nodesInCurrent = current.nodes.Count;
            int nodesInNext = next.nodes.Count; 

            List<GameObject> connectedNodes = new List<GameObject>();

            foreach (GameObject node in current.nodes)
            {
                List<GameObject> nodesToConnectTo = new List<GameObject>();
                GetNode(connectedNodes, next, nodesToConnectTo);
                
                if (nodesInCurrent < nodesInNext)
                {
                    GetNode(connectedNodes, next, nodesToConnectTo);
                }

                foreach (GameObject nextnode in nodesToConnectTo)
                {
                    nextnode.GetComponent<PlayerNode>().connectedNodes.Add(node.GetComponent<PlayerNode>());

                    DrawPath newpath = Instantiate(path);
                    newpath.transform.parent = pathParent.transform;
                    newpath.Draw(node, nextnode);
                }
            }
        }
    }

    public GameObject GetRandomNode(List<GameObject> nodes)
    {
        return nodes[Random.Range(0, nodes.Count)];
    }

    public void GetNode(List<GameObject> connectedNodes, Level next, List<GameObject> nodesToConnectTo)
    {
        int tries = 10;
        GameObject nodeToConnect = GetRandomNode(next.nodes);
        while (connectedNodes.Contains(nodeToConnect) && tries > 0)
        {
            nodeToConnect = GetRandomNode(next.nodes);
            tries--;
        }
        nodesToConnectTo.Add(nodeToConnect);
        connectedNodes.Add(nodeToConnect);
    }

    
}
