using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShadyMan : MonoBehaviour
{
    [SerializeField]
    List<GameObject> optionPrefabs;
    List<GameObject> options;

    [SerializeField] GameObject optionsContainer;
    [SerializeField] GameObject muggedPanel;

    readonly System.Random rand = new();

    void Start()
    {
        // Pick 3 random service options
        if (optionPrefabs.Count < 3) throw new System.Exception();
        options = new();
        List<int> pickedAlready = new();
        while (options.Count < 3)
        {
            int randI;
            do
            {
                randI = rand.Next(optionPrefabs.Count);
            }
            while (pickedAlready.Contains(randI));
            GameObject option = Instantiate(
                original: optionPrefabs[randI],
                position: new(-200f + 200f * options.Count, 0),
                rotation: Quaternion.identity
            );
            option.transform.SetParent(optionsContainer.transform, false);
            var service = option.GetComponent<AbstractShadyManService>();
            if (service.ConditionMet())
            {
                options.Add(option);
                pickedAlready.Add(randI);
            }
        }
    }

    public void ServiceDone(GameObject go)
    {
        StartCoroutine(OptionPickedAnimations(go));
    }

    IEnumerator OptionPickedAnimations(GameObject go)
    {
        SMOptionCard c = go.GetComponent<SMOptionCard>();
        foreach (var o in options)
        {
            if (o != c)
            {
                o.transform.DOLocalMoveY(-1000, 0.5f).SetEase(Ease.InBack);
            }
            else
            {
                o.transform.DOScale(Vector3.one * 1.5f, 0.3f).SetDelay(0.05f);
            }
        }
        yield return new WaitForSeconds(1.0f);
        var r = rand.NextDouble();
        if (r < 0.333)
        {
            StartCoroutine(Mugged(c));
        }
        else
        {
            Exit();
        }
    }

    IEnumerator Mugged(SMOptionCard c)
    {
        //  c.transform.DOLocalMoveY(-500, 0.5f).SetEase(Ease.InBack);
        muggedPanel.SetActive(true);
        float r = UnityEngine.Random.Range(0.0f, 0.3f) * PlayerData.GetInstance().Player.maxHP;
        PlayerData.GetInstance().Player.Damage((int)r);
        muggedPanel.GetComponentInChildren<TMP_Text>().text = "You were mugged! -" + (int)r + "HP! :(";
        c.transform.DOShakePosition(1.0f);
        yield return new WaitForSeconds(2.0f);
        muggedPanel.SetActive(false);
        Exit();
    }

    public void Exit()
    {
        OnuSceneManager.GetInstance().ChangeScene(SceneType.Map);
    }
}
