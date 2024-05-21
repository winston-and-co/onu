using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefeatScreen : MonoBehaviour
{
    [SerializeField] Button nextButton;

    void Awake()
    {
        EventQueue.GetInstance().endBattleEvent.AddListener(OnEndBattle);
        gameObject.SetActive(false);

        nextButton.onClick.AddListener(() => OnuSceneManager.GetInstance().ChangeScene(Scene.Map));
        // Show();
    }

    void OnEndBattle(GameMaster gm)
    {
        if (gm.Victor != gm.Player)
        {
            // TODO: Back to menu?
        }
    }
}