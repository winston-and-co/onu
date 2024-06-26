using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class MapNode : MonoBehaviour
{
    [SerializeField] SceneType nodeType;
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
        if (IsValid())
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
            // AudioSource source = SoundManager.GetInstance().mainSource;
            // source.clip = SoundManager.GetInstance().enterBossNode;
            // source.Play();
        }
        else
        {
            // AudioSource source = SoundManager.GetInstance().mainSource;
            // source.clip = SoundManager.GetInstance().enterNode;
            // source.Play();
        }
        var playerSprite = GameObject.Find("PlayerSprite");
        playerSprite.transform.position = transform.position;

        OnuSceneManager.GetInstance().ChangeScene(nodeType);
    }
}
