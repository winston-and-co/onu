using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerNode : MonoBehaviour
{
    public bool startingNode;
    public bool crownNode;
    public List<PlayerNode> connectedNodes = new List<PlayerNode>();

    void OnMouseDown()
    {
        Player player = FindObjectOfType<Player>();
        if (connectedNodes.Contains(player.currentnode))
        {
            ChangeScene(player);
        }
        if (player.currentnode == null && startingNode)
        {
            ChangeScene(player);
        }


    }

    public void ChangeScene(Player player)
    {
        FindObjectOfType<Player>().transform.position = transform.position;
        player.currentnode = this;
        if (crownNode)
        {
            FindObjectOfType<SoundManager>().PlayEnteringCrownNodeSound();
        }
        else
        {
            FindObjectOfType<SoundManager>().PlayEnteringSound();
        }

        GetComponent<SceneChanger>().ChangeScene();
    }
}
