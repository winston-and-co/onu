using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.VFX;

public class Level : MonoBehaviour
{
    public int levelIdx;
    public List<MapNode> nodes = new();
    [SerializeField] List<GameObject> nodeTypes;
    [SerializeField] Vector2 spawnRegion;
    [SerializeField] float minDistance;
    [SerializeField] List<float> positions;
    [SerializeField] bool startingLevelNodes;
    [SerializeField] int minNodes = 3;
    [SerializeField] int maxNodes = 5;

    public void Generate()
    {
        positions.Add(999);
        int nodeCount = Random.Range(minNodes, maxNodes + 1);
        for (int i = 0; i < nodeCount; i++)
        {
            float spawnOffset = Random.Range(spawnRegion.x, spawnRegion.y);
            float distance = positions.Min(x => System.Math.Abs(spawnOffset - x));
            while (distance <= minDistance)
            {
                spawnOffset = Random.Range(spawnRegion.x, spawnRegion.y);
                distance = positions.Min(x => System.Math.Abs(spawnOffset - x));
            }

            int index = Random.Range(0, nodeTypes.Count);
            MapNode node = Instantiate(nodeTypes[index])
                .GetComponent<MapNode>();
            if (startingLevelNodes)
            {
                node.IsStartingNode = true;
            }
            node.transform.parent = transform;
            node.transform.position = new Vector2(node.transform.position.x + spawnOffset, transform.position.y);
            positions.Add(spawnOffset);
            nodes.Add(node);
            node.Location = (levelIdx, i);
        }
    }

}
