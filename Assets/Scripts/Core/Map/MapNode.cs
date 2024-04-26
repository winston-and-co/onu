using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MapNode : MonoBehaviour
{
    [SerializeField] Scenes nodeType;
    public bool IsStartingNode;
    public bool IsBossNode;
    /// <summary>
    /// Tuple of (level index, node index)
    /// </summary>
    public (int, int) Location;
    [SerializeField] List<MapNode> _connectedNodes;
    public ReadOnlyCollection<MapNode> ConnectedNodes => _connectedNodes.AsReadOnly();

    void OnMouseDown()
    {
        if(IsValid())
        {
            Visit();
        }
    }

    public bool IsValid()
    {
        var pd = PlayerData.GetInstance();
        if ((pd.CurrentNode == null && this.IsStartingNode) ||
            (pd.CurrentNode != null && pd.CurrentNode.ConnectedNodes.Contains(this)))
        {
            return true;
        }
        return false;
    }

    public bool AddConnection(MapNode m)
    {
        if (m == this) return false;
        if (_connectedNodes.Contains(m)) return false;
        _connectedNodes.Add(m);
        return true;
    }

    public void Visit()
    {
        PlayerData.GetInstance().CurrentNodeLocation = Location;
        if (IsBossNode)
        {
            FindObjectOfType<SoundManager>().PlayEnteringCrownNodeSound();
        }
        else
        {
            FindObjectOfType<SoundManager>().PlayEnteringSound();
        }
        var playerSprite = GameObject.Find("PlayerSprite");
        playerSprite.transform.position = transform.position;

        OnuSceneManager.GetInstance().ChangeSceneWithDelay(nodeType, 1);
    }
}
