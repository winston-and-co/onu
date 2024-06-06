using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefeatScreen : MonoBehaviour
{
    [SerializeField] Button nextButton;

    void Awake()
    {
        EventManager.endedBattleEvent.AddListener(OnEndBattle);
        gameObject.SetActive(false);

        nextButton.onClick.AddListener(() => OnuSceneManager.GetInstance().ChangeScene(SceneType.Map));
        // Show();
    }

    void OnEndBattle()
    {
        var gm = GameMaster.GetInstance();
        if (gm.Victor != gm.Player)
        {
            // TODO: Back to menu?
        }
    }
}