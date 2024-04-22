using UnityEngine;
using UnityEngine.UI;

public class ChangeSceneButton : MonoBehaviour
{
    [SerializeField] Scenes target;

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnMouseDown);
    }

    public void OnMouseDown()
    {
        OnuSceneManager.GetInstance().ChangeScene(target);
    }
}