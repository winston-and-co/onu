using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConfirmCharSelectButton : MonoBehaviour, IPointerClickHandler
{
    void Start()
    {
        GetComponent<Button>().interactable = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var cs = FindObjectOfType<CharacterSelect>();
        cs.ConfirmSelection();

        PlayerData.GetInstance().Player.gameObject.SetActive(false);
        PlayerData.GetInstance().Player.deck.gameObject.SetActive(false);

        OnuSceneManager.GetInstance().ChangeScene(Scene.Map);
    }
}