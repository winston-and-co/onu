using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public List<GameObject> nodeTypes;


    void Start()
    {
        int nodeCount = Random.Range(2,4);
        for(int i = 0; i < nodeCount; i++)
        {
            int index = Random.Range(0,nodeTypes.Count);
            GameObject node = Instantiate(nodeTypes[index]);
            node.transform.parent = transform;
            
        }    
    }

}
