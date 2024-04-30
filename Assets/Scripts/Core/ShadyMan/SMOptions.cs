using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static TreeEditor.TreeEditorHelper;

public class SMOptions : MonoBehaviour
{

    public TextMeshProUGUI textMeshProUGUI;
    public GameObject panel;

    public List<SMOptionCard> options;
    // Start is called before the first frame update
    public void OptionPicked(GameObject go)
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
                o.transform.DOLocalMoveY(-500, 0.5f).SetEase(Ease.InBack);

            }
            else
            {
                o.transform.DOScale(Vector3.one * 1.5f, 0.3f).SetDelay(0.05f);
            }
        }
        yield return new WaitForSeconds(1.0f);
        float r = UnityEngine.Random.value;
        if(r < 0.333)
        {
            StartCoroutine(Lost(c));
            //yield return new WaitForSeconds(3.0f);
        } else
        {

        }
        OnuSceneManager.GetInstance().ChangeScene(Scene.Map);

    }
    IEnumerator Lost(SMOptionCard c)
    {
        //  c.transform.DOLocalMoveY(-500, 0.5f).SetEase(Ease.InBack);
        panel.SetActive(true);
        float r = UnityEngine.Random.Range(0.0f, 0.3f) * PlayerData.GetInstance().Player.maxHP;
        PlayerData.GetInstance().Player.Damage((int)r);
        textMeshProUGUI.text = "You were mugged! -" + (int)r + "HP! :(";
        c.transform.DOShakePosition(1.0f);
        yield return new WaitForSeconds(2.0f);
        panel.SetActive(false);

    }
}
