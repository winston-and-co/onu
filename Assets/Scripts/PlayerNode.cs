using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerNode : MonoBehaviour
{
    public bool startingNode;
    public List<PlayerNode> connectedNodes = new List<PlayerNode>();

    void OnMouseDown()
    {
        Player player = FindObjectOfType<Player>();
        if (connectedNodes.Contains(player.currentnode))
        {
            FindObjectOfType<Player>().transform.position = transform.position;
            player.currentnode = this;
            GetComponent<SceneChanger>().ChangeScene();
        }
        if (player.currentnode == null && startingNode)
        {
            FindObjectOfType<Player>().transform.position = transform.position;
            player.currentnode = this;
            GetComponent<SceneChanger>().ChangeScene();
        }


    }
}
