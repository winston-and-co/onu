using UnityEngine;
using UnityEngine.UI;

// TEMP
public class ChangeSceneButton : MonoBehaviour
{
    [SerializeField] Scene target;

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnMouseDown);
    }

    public void OnMouseDown()
    {
        OnuSceneManager.GetInstance().ChangeScene(target);
    }
}