using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterSelectButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject statsPanel;
    [SerializeField] TMP_Text statsTextBody;
    [SerializeField] Button confirmButton;
    [SerializeField] PlayerCharacter character;

    public void OnPointerClick(PointerEventData eventData)
    {
        var cs = FindObjectOfType<CharacterSelect>();
        var preview = cs.PreviewPlayerCharacter(character);

        statsPanel.SetActive(true);
        statsTextBody.text = $@"Character 1

Max HP: {preview.maxHP}
Max Mana: {preview.maxMana}

Starting Hand Size: 7

Deck:
2x 0 RBGY
2x 1 RBGY
2x 2 RBGY
2x 3 RBGY
2x 4 RBGY
2x 5 RBGY
2x 6 RBGY
2x 7 RBGY
2x 8 RBGY
2x 9 RBGY";
        confirmButton.interactable = true;
    }
}