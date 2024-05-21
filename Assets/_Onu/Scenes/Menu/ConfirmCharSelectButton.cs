using UnityEngine;
using UnityEngine.UI;

public class ConfirmCharSelectButton : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void Start()
    {
        GetComponent<Button>().interactable = false;
    }

    void OnClick()
    {
        var cs = FindObjectOfType<CharacterSelect>();
        cs.ConfirmSelection();

        PlayerData.GetInstance().Player.gameObject.SetActive(false);
        PlayerData.GetInstance().Player.deck.gameObject.SetActive(false);

        OnuSceneManager.GetInstance().ChangeScene(Scene.Map);
    }
}