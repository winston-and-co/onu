using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerNode : MonoBehaviour
{
    public List<PlayerNode> connectedNodes = new List<PlayerNode>();

    void OnMouseDown()
    {
        Player player = FindObjectOfType<Player>();
        if (connectedNodes.Contains(player.currentnode))
        {
            FindObjectOfType<Player>().transform.position = transform.position;
            player.currentnode = this;
        }
        if (player.currentnode == null)
        {
            FindObjectOfType<Player>().transform.position = transform.position;
            player.currentnode = this;
        }


    }
}
