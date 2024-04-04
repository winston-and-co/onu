using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;

public class Level : MonoBehaviour
{
    public List<GameObject> nodeTypes;
    public List<GameObject> nodes = new List<GameObject>();
    public Vector2 spawnRegion;
    public float minDistance;
    public List<float> positions;


    void Awake()
    {
        positions.Add(999);
        int nodeCount = Random.Range(2,4);
        for(int i = 0; i < nodeCount; i++)
        {
            float spawnOffset = Random.Range(spawnRegion.x, spawnRegion.y);
            float distance = positions.Min(x => System.Math.Abs(spawnOffset-x));
            while (distance <= minDistance)
            {
                spawnOffset = Random.Range(spawnRegion.x, spawnRegion.y);
                distance = positions.Min(x => System.Math.Abs(spawnOffset - x));
            }

            int index = Random.Range(0,nodeTypes.Count);
            GameObject node = Instantiate(nodeTypes[index]);
            node.transform.parent = transform;
            node.transform.position = new Vector2(node.transform.position.x + spawnOffset, transform.position.y);
            positions.Add(spawnOffset);
            nodes.Add(node);
        }    
    }

}
