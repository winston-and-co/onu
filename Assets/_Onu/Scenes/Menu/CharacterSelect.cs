using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public UnityEvent OnConfirmSelection;
    PlayerPreview preview;

    [SerializeField] GameObject previewPanel;
    [SerializeField] TMP_Text statsTextBody;
    [SerializeField] Button confirmButton;

    public void ConfirmSelection()
    {
        if (PlayerData.GetInstance().Player != null)
            throw new System.Exception("Why are there 2 players???");
        if (preview == null)
            return;
        var pd = PlayerData.GetInstance();
        if (pd.Player == null)
        {
            pd.Player = preview.ToPlayerEntity();
            foreach (var rc in preview.StarterRuleCardInstances)
            {
                PlayerData.GetInstance().AddRuleCard(rc);
            }
            foreach (var ac in preview.StarterActionCardInstances)
            {
                PlayerData.GetInstance().AddActionCard(ac);
            }
            DontDestroyOnLoad(pd.Player);
        }
        OnConfirmSelection.Invoke();
    }

    public void PreviewPlayerCharacter(PlayerCharacter character)
    {
        PlayerPreview preview = character switch
        {
            PlayerCharacter.Character1 => PlayerPreviewFactory.MakePlayerPreview(PlayerCharacter.Character1),
            _ => throw new ArgumentException(),
        };
        this.preview = preview;
        OnPreview();
    }

    void OnPreview()
    {
        previewPanel.SetActive(true);
        statsTextBody.text = preview.ToString();
        confirmButton.interactable = true;
    }
}