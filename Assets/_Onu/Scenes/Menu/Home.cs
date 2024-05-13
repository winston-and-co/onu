using UnityEngine;

public class Home : MonoBehaviour
{
    [SerializeField] GameObject charSelectPanel;
    void OnEnable()
    {
        charSelectPanel.SetActive(false);
    }
}