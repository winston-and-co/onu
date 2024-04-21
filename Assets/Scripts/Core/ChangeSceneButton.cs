using UnityEngine;

public class ChangeSceneButton : MonoBehaviour
{
    [SerializeField] Scenes target;

    void OnMouseDown()
    {
        OnuSceneManager.ChangeScene(target);
    }
}