using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterSelectButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] CharacterSelect characterSelect;
    [SerializeField] PlayerCharacter character;

    public void OnPointerClick(PointerEventData eventData)
    {
        characterSelect.PreviewPlayerCharacter(character);
    }
}